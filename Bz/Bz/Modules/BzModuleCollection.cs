using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Modules
{
    /// <summary>
    /// 用于存储一个字典型模块List<BzModuleInfo>
    /// </summary>
    internal class BzModuleCollection:List<BzModuleInfo>
    {
        /// <summary>
        /// 获取一个模块
        /// </summary>
        /// <typeparam name="TModule">Module Type</typeparam>
        /// <returns>模块的实例</returns>
        public TModule GetModule<TModule>() where TModule : BzModule
        {
            var module = this.FirstOrDefault(m => m.Type == typeof(TModule));
            if (module==null)
            {
                throw new BzException("未能找到模块："+typeof(TModule).FullName);
            }
            return (TModule)module.Instance;
        }

        /// <summary>
        /// 按照依赖的顺序设置Module
        /// 如果模块B依赖于模块Z，则B会在Z后面加到列表
        /// </summary>
        /// <returns>排序列表</returns>
        public List<BzModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            return sortedModules;
        }


        private static void EnsureKernelModuleToBeFirst(List<BzModuleInfo> sortedModules)
        {
            var kernelModuleIndex = sortedModules.FindIndex(x=>x.Type==typeof(BzKernelModule));
            if (kernelModuleIndex>0)
            {
                var kernelModule = sortedModules[kernelModuleIndex];
                sortedModules.RemoveAt(kernelModuleIndex);
                sortedModules.Insert(0, kernelModule);
            }
        }
    }
}
