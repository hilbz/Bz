using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 工作单元选项
    /// </summary>
    public class UnitOfWorkOptions
    {
        /// <summary>
        /// 作用域选项.
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// 是否启用事务?
        /// 默认不支持.
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// 事务超时时间.
        /// 默认不支持.
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 事务隔离级别
        /// 默认不支持.
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 该选项会被设置成 <see cref="TransactionScopeAsyncFlowOption.Enabled"/>
        /// 是否启用跨线程连续任务事务流
        /// </summary>
        public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }

        /// <summary>
        /// 启用/禁用一些过滤
        /// </summary>
        public List<DataFilterConfiguration> FilterOverrides { get; private set; }

        public UnitOfWorkOptions()
        {
            FilterOverrides = new List<DataFilterConfiguration>();
        }

        internal void FillDefaultsForNonProvidedOptions(IUnitOfWorkDefaultOptions defaultOptions)
        {
            //TODO: Do not change options object..?

            if (!IsTransactional.HasValue)
            {
                IsTransactional = defaultOptions.IsTransactional;
            }

            if (!Scope.HasValue)
            {
                Scope = defaultOptions.Scope;
            }

            if (!Timeout.HasValue && defaultOptions.Timeout.HasValue)
            {
                Timeout = defaultOptions.Timeout.Value;
            }

            if (!IsolationLevel.HasValue && defaultOptions.IsolationLevel.HasValue)
            {
                IsolationLevel = defaultOptions.IsolationLevel.Value;
            }
        }
    }
}
