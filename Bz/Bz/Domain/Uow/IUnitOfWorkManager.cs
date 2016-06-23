using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bz.Domain.Uow
{

    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// 获取当前的工作单元（不存在返回空）
        /// </summary>
        IActiveUnitOfWork Current { get; }

        /// <summary>
        /// 开始一个新的工作单元
        /// </summary>
        /// <returns>返回一个UOW完成时处理</returns>
        IUnitOfWorkCompleteHandle Begin();

        /// <summary>
        /// 开始一个新的工作单元
        /// </summary>
        /// <returns>返回一个UOW完成时处理</returns>
        IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope);

        /// <summary>
        /// 开始一个新的工作单元
        /// </summary>
        /// <returns>返回一个UOW完成时处理</returns>
        IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options);
    }
}
