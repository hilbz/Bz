using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    /// <summary>
    /// 定义一个用来被注入类的接口
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// 添加一个约定注入
        /// </summary>
        /// <param name="registrar"></param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        /// 对程序集中约定类进行注入
        /// </summary>
        /// <param name="assembly"></param>
        void RegisterAssemblyByConvention(Assembly assembly);

        /// <summary>
        /// 对程序集中约定类进行注入 <see cref=""/>
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="config">配置：安装拦截器</param>
        void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config);


        /// <summary>
        /// 自我实例化(泛型版)
        /// </summary>
        /// <typeparam name="T">Type Of class</typeparam>
        /// <param name="lifeStyle"></param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T:class;

        /// <summary>
        /// 自我实例化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lifeStyle"></param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// 对接口设计进行注册实现
        /// </summary>
        /// <typeparam name="TType">IBzService</typeparam>
        /// <typeparam name="TImpl">BzService</typeparam>
        /// <param name="lifeStyle"></param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// 对接口设计进行注册实现
        /// </summary>
        /// <param name="type">Type Of Class</param>
        /// <param name="impl">The Type that implements</param>
        /// <param name="lifeStyle">注册生命周期类型</param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// 判断给定的类是否注册
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        bool IsRegistered(Type type);

        /// <summary>
        /// 判断给定的类是否注册
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        bool IsRegistered<TType>();
    }
}
