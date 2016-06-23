using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 该接口用于当前被激活的工作单元
    /// 此接口不能直接被用
    /// 用<see cref="IUnitOfWorkManager"/>代替
    /// </summary>
    public interface IActiveUnitOfWork
    {
        /// <summary>
        /// UOW成功完成时触发
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// UOW失败时触发事件。
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// UOW Dispose触发.
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// Gets 如果当前工作单元是事务级别的.
        /// </summary>
        UnitOfWorkOptions Options { get; }

        /// <summary>
        /// 工作单元的数据过滤器配置.
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// UOW是否已经被释放
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// 提交，可以在任何是时候
        /// 一般情况不用直接调用此方法
        /// 在UOW结束时候会自动进行提交
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// 提交，可以在任何是时候
        /// 一般情况不用直接调用此方法
        /// 在UOW结束时候会自动进行提交
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// 禁用一个或多个过滤器.
        /// 如果已经禁用了，则不处理. 
        /// Use this method in a using statement to re-enable filters if needed.
        /// </summary>
        /// <param name="filterNames">一个或多个过滤器名称. <see cref="BzDataFilters"/> for standard filters.</param>
        /// <returns>A <see cref="IDisposable"/> .</returns>
        IDisposable DisableFilter(params string[] filterNames);

        /// <summary>
        /// 启用一个或多个过滤器.
        ///  如果已经启用了，则不处理. 
        /// Use this method in a using statement to re-disable filters if needed.
        /// </summary>
        /// <param name="filterNames">一个或多个过滤器名称. <see cref="AbpDataFilters"/> for standard filters.</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the enable effect.</returns>
        IDisposable EnableFilter(params string[] filterNames);

        /// <summary>
        /// 判断特定过滤器是否启用或则禁用.
        /// </summary>
        /// <param name="filterName">过滤器名称. <see cref="BzDataFilters"/> for standard filters.</param>
        bool IsFilterEnabled(string filterName);

        /// <summary>
        /// 设置或则重写一个过滤器的参数.
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数需要被设置的值</param>
        IDisposable SetFilterParameter(string filterName, string parameterName, object value);

    }
}
