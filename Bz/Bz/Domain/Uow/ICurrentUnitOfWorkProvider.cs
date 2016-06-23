using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 获取当前的工作单元<see cref="IUnitOfWork"/>
    /// </summary>
    public interface ICurrentUnitOfWorkProvider
    {
        /// <summary>
        /// 获取/设置<see cref="IUnitOfWork"/>.
        /// </summary>
        IUnitOfWork Current { get; set; }
    }
}
