using Bz.Configuration.Startup;
using Bz.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bz.Modules
{

    /// <summary>
    /// 该类必须被所有模块系统来进行实现
    /// 一个模块定义类独立自己的程序集内
    /// 而且要实现一个模块事件的动作在 application startup and shotdown
    /// 它也定义了一些依赖的模块
    /// </summary>
    public abstract class BzModule
    {
        /// <summary>
        /// 获取一个IocManager的引用
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        /// Bz StartupConfig配置
        /// </summary>
        protected internal IBzStartupConfiguration Configuration { get; internal set; }

        /// <summary>
        /// Application启动的第一调用事件
        /// 此段代码里面运行会再DI注册之前进行调用
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// 用于注册模块DI System.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// 该方法在application startup之后调用
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 该方法会在应用程序员结束的时候调用
        /// </summary>
        public virtual void Shutdown()
        {

        }

        /// <summary>
        /// 判断给定的Type是不是Bz Module 类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBzModule(Type type)
        {
            return
                type.IsClass &&
                !type.IsAbstract &&
                typeof(BzModule).IsAssignableFrom(type);
                    
        }

        /// <summary>
        /// 查询一个模块Module所依赖的模块
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsBzModule(moduleType))
                throw new BzInitializationException("该类型不是Bz Module:"+moduleType.AssemblyQualifiedName);

            var list = new List<Type>();

            if (moduleType.IsDefined(typeof(DependsOnAttribute),true))
            {
                var dependsOnAttributes = moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }
            return list;
        }
    }
}
