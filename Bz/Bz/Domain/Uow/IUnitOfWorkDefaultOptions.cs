using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 用于获取一个工作单元默认的配置
    /// </summary>
    public interface IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// 事务范畴附加选项
        /// </summary>
        TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// 指明当前工作单元是否是个事务
        /// Default: true.
        /// </summary>
        bool IsTransactional { get; set; }

        /// <summary>
        /// 获取/设置工作单元超时时间.
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 获取和设置工作单元的事务级别.
        /// This is used if <see cref="IsTransactional"/> is true.
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 所有数据过滤器配置列表
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// 在工作单元内注册一个过滤器
        /// </summary>
        /// <param name="filterName">过滤器名称.</param>
        /// <param name="isEnabledByDefault">默认是否启用.</param>
        void RegisterFilter(string filterName, bool isEnabledByDefault);

        /// <summary>
        /// 重写数据过滤定义.
        /// </summary>
        /// <param name="filterName">过滤器名称.</param>
        /// <param name="isEnabledByDefault">默认是否启用.</param>
        void OverrideFilter(string filterName, bool isEnabledByDefault);

    }
}
