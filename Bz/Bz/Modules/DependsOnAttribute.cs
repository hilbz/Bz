using System;

namespace Bz.Modules
{
    /// <summary>
    /// 用来指定Bz Module的依赖模块关系 to other modules<see cref=""/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =true)]
    public class DependsOnAttribute:Attribute
    {
        /// <summary>
        /// 需要依赖的模块类型
        /// </summary>
        public Type[] DependedModuleTypes { get; set; }

        /// <summary>
        /// 用于定义一些依赖Bz Module to other modules
        /// </summary>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
