using Bz.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Policies
{

    /// <summary>
    /// 该类可以被所有代理类根据约定实现
    /// </summary>
    public interface IPolicy:ITransientDependency
    {
    }
}
