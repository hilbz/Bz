using Bz.Collections;
using Bz.Dependency;
using Bz.Modules;
using Bz.Runtime.Session;
using Bz.TestBase.Modules;
using Bz.TestBase.Runtime.Session;
using System;

namespace Bz.TestBase
{
    /// <summary>
    /// Bz系统统一的单元测试类的基类
    /// </summary>
    public abstract class BzIntegratedTestBase : IDisposable
    {

        /// <summary>
        ///单元测试引用IocManager.
        /// </summary>
        protected IIocManager LocalIocManager { get; private set; }

        protected TestBzSession BzSession { get; private set; }

        private readonly BzBootstrapper _bootstrapper;

        protected BzIntegratedTestBase()
        {
            LocalIocManager = new IocManager();

            LocalIocManager.Register<IModuleFinder, TestModuleFinder>();
            LocalIocManager.Register<IBzSession, TestBzSession>();

            AddModules(LocalIocManager.Resolve<TestModuleFinder>().Modules);

            PreInitialize();

            _bootstrapper = new BzBootstrapper(LocalIocManager);
            _bootstrapper.Initialize();

            BzSession = LocalIocManager.Resolve<TestBzSession>();
        }

        protected virtual void AddModules(ITypeList<BzModule> modules)
        {
            modules.Add<TestBaseModule>();
        }

        /// 该方法能被重写对一些services fakes.
        /// </summary>
        protected virtual void PreInitialize()
        {

        }

        public virtual void Dispose()
        {
            _bootstrapper.Dispose();
            LocalIocManager.Dispose();
        }

        protected T Resolve<T>()
        {
            EnsureClassRegistered(typeof(T));
            return LocalIocManager.Resolve<T>();
        }

        protected T Resolve<T>(object argumentsAsAnonymousType)
        {
            EnsureClassRegistered(typeof(T));
            return LocalIocManager.Resolve<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        ///简写 <see cref="LocalIocManager"/>.
        ///如果没有注册过，则注册成临时类
        /// </summary>
        /// <param name="type">需要注册的类型</param>
        /// <returns>对象的实例</returns>
        protected object Resolve(Type type)
        {
            EnsureClassRegistered(type);
            return LocalIocManager.Resolve(type);
        }

        /// <summary>
        ///简写 <see cref="LocalIocManager"/>.
        ///如果没有注册过，则注册成临时类
        /// </summary>
        /// <param name="type">需要注册的类型</param>
        /// <param name="argumentsAsAnonymousType">构造函数</param>
        /// <returns>对象的实例</returns>
        protected object Resolve(Type type, object argumentsAsAnonymousType)
        {
            EnsureClassRegistered(type);
            return LocalIocManager.Resolve(type, argumentsAsAnonymousType);
        }
        /// <summary>
        /// 给定的类是否已经被注册
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lifeStyle"></param>
        protected void EnsureClassRegistered(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Transient)
        {
            if (!LocalIocManager.IsRegistered(type))
            {
                if (!type.IsClass || type.IsAbstract)
                {
                    throw new BzException("不能注册：" + type.Name + ".不能是抽象类，或则应该被注册过了");
                }
            }
            LocalIocManager.Register(type, lifeStyle);
        }
    }
}
