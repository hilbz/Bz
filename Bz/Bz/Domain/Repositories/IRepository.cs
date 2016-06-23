using Bz.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Repositories
{
    /// <summary>
    /// 该接口必须被所有仓储类继承
    /// 用泛型版替代此类
    /// </summary>
    public interface IRepository:ITransientDependency
    {
    }
}
