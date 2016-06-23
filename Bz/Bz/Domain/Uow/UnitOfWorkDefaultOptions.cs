using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Bz.Domain.Uow
{
    internal class UnitOfWorkDefaultOptions:IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// 事务范畴附加选项
        /// </summary>
        public TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// 指明当前工作单元是否是个事务
        /// Default: true.
        /// </summary>
        public bool IsTransactional { get; set; }

        /// <summary>
        /// 获取/设置工作单元超时时间.
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 获取和设置工作单元的事务级别.
        /// This is used if <see cref="IsTransactional"/> is true.
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 所有数据过滤器配置列表
        /// </summary>
        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters; }
        }
        private readonly List<DataFilterConfiguration> _filters;

        public void RegisterFilter(string filterName, bool isEnabledByDefault)
        {
            if (_filters.Any(f => f.FilterName == filterName))
            {
                throw new BzException("已经存在一个过滤器: " + filterName);
            }

            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        public void OverrideFilter(string filterName, bool isEnabledByDefault)
        {
            _filters.RemoveAll(f => f.FilterName == filterName);
            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }


        public UnitOfWorkDefaultOptions()
        {
            _filters = new List<DataFilterConfiguration>();
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;
        }

    }
}
