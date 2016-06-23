using Bz.MultiTenancy;
using Bz.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// Uow处理类的基类
    /// </summary>
    public abstract class UnitOfWorkBase:IUnitOfWork
    {
        public string Id { get; private set; }

        public IUnitOfWork Outer { get; set; }

        /// <inheritdoc/>
        public event EventHandler Completed;

        /// <inheritdoc/>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <inheritdoc/>
        public event EventHandler Disposed;

        /// <inheritdoc/>
        public UnitOfWorkOptions Options { get; private set; }

        /// <inheritdoc/>
        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters.ToImmutableList(); }
        }
        private readonly List<DataFilterConfiguration> _filters;

        /// <summary>
        ///获取UOW默认选项.
        /// </summary>
        protected IUnitOfWorkDefaultOptions DefaultOptions { get; private set; }

        /// <summary>
        /// 指示工作单元是否已被释放.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 引用当前 Bz session.
        /// </summary>
        public IBzSession BzSession { private get; set; }

        /// <summary>
        /// 是否 <see cref="Begin"/> 方法执行之前?
        /// </summary>
        private bool _isBeginCalledBefore;

        /// <summary>
        /// 是否 <see cref="Complete"/>方法执行之前?
        /// </summary>
        private bool _isCompleteCalledBefore;

        /// <summary>
        /// UOW是否成功Complete了.
        /// </summary>
        private bool _succeed;

        /// <summary>
        /// 工作单元异常.
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected UnitOfWorkBase(IUnitOfWorkDefaultOptions defaultOptions)
        {
            DefaultOptions = defaultOptions;

            Id = Guid.NewGuid().ToString("N");
            _filters = defaultOptions.Filters.ToList();
            BzSession = NullBzSession.Instance;
        }

        /// <inheritdoc/>
        public abstract void SaveChanges();

        /// <inheritdoc/>
        public abstract Task SaveChangesAsync();

        /// <inheritdoc/>
        public IDisposable DisableFilter(params string[] filterNames)
        {
            //TODO: Check if filters exists?

            var disabledFilters = new List<string>();

            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (_filters[filterIndex].IsEnabled)
                {
                    disabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(filterName, false);
                }
            }

            disabledFilters.ForEach(ApplyDisableFilter);

            return new DisposeAction(() => EnableFilter(disabledFilters.ToArray()));
        }

        /// <inheritdoc/>
        public IDisposable EnableFilter(params string[] filterNames)
        {
            //TODO: Check if filters exists?

            var enabledFilters = new List<string>();

            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (!_filters[filterIndex].IsEnabled)
                {
                    enabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(filterName, true);
                }
            }

            enabledFilters.ForEach(ApplyEnableFilter);

            return new DisposeAction(() => DisableFilter(enabledFilters.ToArray()));
        }


        /// <inheritdoc/>
        public bool IsFilterEnabled(string filterName)
        {
            return GetFilter(filterName).IsEnabled;
        }

        /// <inheritdoc/>
        public void Begin(UnitOfWorkOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            PreventMultipleBegin();
            Options = options; //TODO: Do not set options like that, instead make a copy?

            SetFilters(options.FilterOverrides);

            BeginUow();
        }

        /// <inheritdoc/>
        public void Complete()
        {
            PreventMultipleComplete();
            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task CompleteAsync()
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }
        /// <summary>
        /// 继承类必须实现 start UOW.
        /// </summary>
        protected abstract void BeginUow();

        /// <summary>
        /// 继承类必须实现 complete UOW.
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// 继承类必须实现 complete UOW.
        /// </summary>
        protected abstract Task CompleteUowAsync();

        /// <summary>
        /// 继承类必须实现 dispose UOW.
        /// </summary>
        protected abstract void DisposeUow();

        /// <inheritdoc/>
        public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        {
            var filterIndex = GetFilterIndex(filterName);

            var newfilter = new DataFilterConfiguration(_filters[filterIndex]);

            //Store old value
            object oldValue = null;
            var hasOldValue = newfilter.FilterParameters.ContainsKey(filterName);
            if (hasOldValue)
            {
                oldValue = newfilter.FilterParameters[filterName];
            }

            newfilter.FilterParameters[parameterName] = value;

            _filters[filterIndex] = newfilter;

            ApplyFilterParameterValue(filterName, parameterName, value);

            return new DisposeAction(() =>
            {
                //Restore old value
                if (hasOldValue)
                {
                    SetFilterParameter(filterName, parameterName, oldValue);
                }
            });
        }

        /// <summary>
        /// 实现类必须实现此方法
        /// 该方法用来禁用一个过滤器.
        /// 不要调用父类方法 <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="filterName">过滤器名</param>
        protected virtual void ApplyDisableFilter(string filterName)
        {
            throw new NotImplementedException("DisableFilter 未实现： " + GetType().FullName);
        }

        /// <summary>
        /// 实现类必须实现此方法
        /// 该方法用来启用一个过滤器.
        /// 不要调用父类方法 <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="filterName">过滤器名</param>
        protected virtual void ApplyEnableFilter(string filterName)
        {
            throw new NotImplementedException("EnableFilter 未实现： " + GetType().FullName);
        }

        /// <summary>
        /// 实现类必须实现此方法
        /// 该方法用来设置过滤的参数值.
        /// 不要调用父类方法 <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="filterName">过滤器名</param>
        protected virtual void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            throw new NotImplementedException("SetFilterParameterValue 未实现： " + GetType().FullName);
        }

        /// <summary>
        /// Called to trigger <see cref="Completed"/> event.
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        /// Called to trigger <see cref="Failed"/> event.
        /// </summary>
        /// <param name="exception">Exception that cause failure</param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// Called to trigger <see cref="Disposed"/> event.
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }


        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new BzException("工作单元已经启动，不可以多次启动.");
            }

            _isBeginCalledBefore = true;
        }
        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new BzException("工作单元已经完成了!");
            }

            _isCompleteCalledBefore = true;
        }


        private void SetFilters(List<DataFilterConfiguration> filterOverrides)
        {
            for (var i = 0; i < _filters.Count; i++)
            {
                var filterOverride = filterOverrides.FirstOrDefault(f => f.FilterName == _filters[i].FilterName);
                if (filterOverride != null)
                {
                    _filters[i] = filterOverride;
                }
            }

            if (!BzSession.UserId.HasValue || BzSession.MultiTenancySide == MultiTenancySides.Host)
            {
                ChangeFilterIsEnabledIfNotOverrided(filterOverrides, BzDataFilters.MustHaveTenant, false);
            }
        }

        private void ChangeFilterIsEnabledIfNotOverrided(List<DataFilterConfiguration> filterOverrides, string filterName, bool isEnabled)
        {
            if (filterOverrides.Any(f => f.FilterName == filterName))
            {
                return;
            }

            var index = _filters.FindIndex(f => f.FilterName == filterName);
            if (index < 0)
            {
                return;
            }

            if (_filters[index].IsEnabled == isEnabled)
            {
                return;
            }

            _filters[index] = new DataFilterConfiguration(filterName, isEnabled);
        }

        private DataFilterConfiguration GetFilter(string filterName)
        {
            var filter = _filters.FirstOrDefault(f => f.FilterName == filterName);
            if (filter == null)
            {
                throw new BzException("未能识别的过滤器名: " + filterName + ". 确定该过滤器是否已经注册过.");
            }
            return filter;
        }

        private int GetFilterIndex(string filterName)
        {
            var filterIndex = _filters.FindIndex(f => f.FilterName == filterName);
            if (filterIndex < 0)
            {
                throw new BzException("未能识别的过滤器名: " + filterName + ". 确定该过滤器是否已经注册过.");
            }
            return filterIndex;
        }
    }
}
