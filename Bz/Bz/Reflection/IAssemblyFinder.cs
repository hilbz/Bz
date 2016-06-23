using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Reflection
{
    /// <summary>
    /// 用来获取所有的程序集，以便取得程序集内特殊的类(比如：获取所有Module类)
    /// </summary>
    public interface IAssemblyFinder
    {

        /// <summary>
        /// 该方法会返回application所有的程序集
        /// </summary>
        /// <returns></returns>
        List<Assembly> GetAllAssemblies();
    }
}
