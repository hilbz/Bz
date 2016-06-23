using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Modules
{
    /// <summary>
    /// 用来储存一个模块
    /// </summary>
    internal class BzModuleInfo
    {
        /// <summary>
        /// 包含Module定义的程序集
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// 模块的Type
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// 模块的实例
        /// </summary>
        public BzModule Instance { get; private set; }

        /// <summary>
        /// 所有依赖此模块的依赖模块信息
        /// </summary>
        public List<BzModuleInfo> Dependencies { get; private set; }

        /// <summary>
        /// 创建一个新的BzModule
        /// </summary>
        /// <param name="instance"></param>
        public BzModuleInfo(BzModule instance)
        {
            Dependencies = new List<BzModuleInfo>();
            Type = instance.GetType();
            Instance = instance;
            Assembly = Type.Assembly;
        }

        public override string ToString()
        {
            return string.Format("{0}",Type.AssemblyQualifiedName); 
        }
    }
}
