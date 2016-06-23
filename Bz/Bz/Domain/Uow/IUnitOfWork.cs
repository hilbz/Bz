using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 定义工作单元
    /// 用BZ Sysmtem的内部
    /// Use<see cref="IUnit"/>
    /// </summary>
    public interface IUnitOfWork:IActiveUnitOfWork,IUnitOfWorkCompleteHandle
    {
        /// <summary>
        /// UOW的标示
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 如果存在外部的UOW.
        /// </summary>
        IUnitOfWork Outer { get; set; }

        /// <summary>
        /// 开始执行
        /// </summary>
        /// <param name="options"></param>
        void Begin(UnitOfWorkOptions options);
    }
}
