using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    /// <summary>
    /// 用于直接管理依赖注入(DI:dependency injection)系统管理
    /// </summary>
    public class IocManager : IIocManager
    {
        /// <summary>
        /// 单例IocManager
        /// </summary>
        public static IocManager Instance { get; private set; }

        /// <summary>
        /// 引用Castle Windsor Container
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }

        /// <summary>
        /// 需要被约定注册的列表
        /// </summary>       
        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        static IocManager()
        {
            Instance = new IocManager();
        }
        /// <summary>
        /// 创建一个 IocManager Object
        /// 正常情况你无须直接实例此类<see cref="IocManager"/>.
        /// 这可能对Test为目的还是有用的
        /// </summary>
        public IocManager()
        {
            IocContainer = new WindsorContainer();
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();

            //自我注册
            IocContainer.Register(
                Component.For<IocManager, IIocManager, IIocRegistrar, IIocResolver>().UsingFactoryMethod(() => this)
                   );
        }

        /// <summary>
        /// 添加约定的注册
        /// </summary>
        /// <param name="registrar"></param>
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        /// <summary>
        /// 注册给定程序集所有需要约定注册的类
        /// </summary>
        /// <param name="assembly">需要注册的程序集</param>
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            RegisterAssemblyByConvention(assembly, new ConventionalRegistrationConfig());
        }

        /// <summary>
        /// 注册给定程序集所有需要约定注册的类
        /// </summary>
        /// <param name="assembly">给定的程序集</param>
        /// <param name="config">附加的配置</param>
        public void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config)
        {
            var context = new ConventionalRegistrationContext(assembly,this,config);
            foreach (var registerer in _conventionalRegistrars)
            {
                registerer.RegisterAssembly(context);
            }
            if (config.InstallInstallers)
            {
                IocContainer.Install(FromAssembly.Instance(assembly));
            }
        }

        /// <summary>
        /// 自我注册
        /// </summary>
        /// <typeparam name="TType">Type Of Class</typeparam>
        /// <param name="lifeStyle">objects的注入类型：单例Or临时</param>
        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        /// <summary>
        /// 自我注册
        /// </summary>
        /// <param name="type">Type Of Class</param>
        /// <param name="lifeStyle">objects的注入类型：单例Or临时</param>
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        /// <summary>
        /// 注册Type with it's implementation
        /// </summary>
        /// <typeparam name="TType">IBzService</typeparam>
        /// <typeparam name="TImpl">BzService</typeparam>
        /// <param name="lifeStyle">设置生命周期类型，单例Or临时</param>
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType:class
            where TImpl:class,TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType,TImpl>(),lifeStyle));
        }

        /// <summary>
        /// 注册Type with it's implementation
        /// </summary>
        /// <param name="type">IBzService</param>
        /// <param name="impl">BzService</param>
        /// <param name="lifeStyle">设置生命周期类型，单例Or临时</param>
        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

        /// <summary>
        /// 判断一个Type是否已经注册
        /// </summary>
        /// <param name="type">Type to Check</param>
        /// <returns></returns>
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        /// <summary>
        /// 判断一个Type是否已经注册
        /// </summary>
        /// <typeparam name="TType">Type to Check</typeparam>
        /// <returns></returns>
        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        /// <summary>
        /// 从IOC Container获取对象
        /// 返回实体必须被释放在使用之后
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        /// <summary>
        /// 获取一个对象从Ioc Container，一个接口会有多个实例对象.
        /// 对象再用完必须被释放掉
        /// </summary>
        /// <typeparam name="T">Type of the object to get(IController)</typeparam>
        /// <param name="type">BzController</param>
        /// <returns>对象实例</returns>
        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        /// <summary>
        /// 获取一个对象从Ioc Container
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <returns>对象实例</returns>
        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        /// <summary>
        /// 获取一个对象从Ioc Container.
        /// 对象再用完必须被释放掉
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="argumentsAsAnonymousType">构造参数</param>
        /// <returns>对象实例</returns>
        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        /// 获取一个对象从Ioc Container.
        /// 对象再用完必须被释放掉
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <param name="argumentsAsAnonymousType">构造参数</param>
        /// <returns>对象实例</returns>
        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

        /// <summary>
        /// 释放一个对象
        /// </summary>
        /// <param name="obj">需要释放的对象</param>
        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        /// <summary>
        /// 释放IocContainer
        /// </summary>
        public void Dispose()
        {
            IocContainer.Dispose();
        }
        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle liftStyle)
            where T :class
        {
            switch (liftStyle)
            {
                case DependencyLifeStyle.Singleton:
                    //设置对象的生命周期是单例
                    return registration.LifestyleSingleton();
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                default:
                    return registration;
            }
        }


    }
}
