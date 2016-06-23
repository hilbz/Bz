using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Reflection
{
    /// <summary>
    /// 默认的实现<see cref="IAssemblyFinder"/>
    /// 从当前的域获取程序集
    /// </summary>
    public class CurrentDomainAssemblyFinder:IAssemblyFinder
    {
        /// <summary>
        /// 获取默认的程序集查找<see cref="CurrentDomainAssemblyFinder"/>
        /// </summary>
        public static CurrentDomainAssemblyFinder Instance { get { return SingletonInstance; } }

        private static CurrentDomainAssemblyFinder SingletonInstance = new CurrentDomainAssemblyFinder();
        public List<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
    }
}
