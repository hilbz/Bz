using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 用于完成一个工作单元
    /// 该接口不会直接被继承使用
    /// 用<see cref="IUnitOfWorkManager"/>代替
    /// </summary>
    public interface IUnitOfWorkCompleteHandle:IDisposable
    {
        /// <summary>
        /// 完成一个工作单元
        /// 保存所有的改变，如果有事务的则提交事务
        /// </summary>
        void Complete();

        /// <summary>
        /// 完成一个工作单元
        /// 保存所有的改变，如果有事务的则提交事务
        /// </summary>
        Task CompleteAsync();
    }
}
