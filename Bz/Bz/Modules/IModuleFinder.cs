using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Modules
{
    /// <summary>
    /// 此接口用于查找所有模块的Type继承于derived from <see cref="BzModule"/>
    /// 在程序集内
    /// </summary>
    public interface IModuleFinder
    {
        /// <summary>
        /// 查找所有的模块
        /// </summary>
        /// <returns></returns>
        ICollection<Type> FindAll();
    }
}
