using Bz.Configuration.Startup;
using Bz.Dependency;
using Bz.Dependency.Installers;
using Bz.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz
{
    /// <summary>
    /// 这个是一个主要类用于启动BZ System.
    /// 准备DI和注册核心的组件
    /// 必须执行instantiated and initialized 
    /// </summary>
    public class BzBootstrapper:IDisposable
    {

        /// <summary>
        /// 获取IIocManager 通过此类
        /// </summary>
        public IIocManager IocManager { get; set; }

        /// <summary>
        /// 一个类是否被释放
        /// </summary>
        protected bool IsDisposed;

        private IBzModuleManager _moduleManager;

        /// <summary>
        /// 创建新的<see cref="BzBootstrapper"/>
        /// </summary>
        public BzBootstrapper() 
            : this(Dependency.IocManager.Instance)
        {

        }

        /// <summary>
        /// 创建新的<see cref="BzBootstrapper"/>
        /// </summary>
        /// <param name="iocManager"></param>
        public BzBootstrapper(IIocManager iocManager)
        {
            IocManager = iocManager;
        }


        /// <summary>
        /// 初始化Bz System.
        /// </summary>
        public virtual void Initialize()
        {
            IocManager.IocContainer.Install(new BzCoreInstaller());

            IocManager.Resolve<BzStartupConfiguration>().Initialize();

            _moduleManager = IocManager.Resolve<IBzModuleManager>();
            _moduleManager.InitializeModules();
        }
        
        /// <summary>
        /// 在Bz System释放的时候做的事情
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            IsDisposed = true;

            if (_moduleManager!=null)
            {
                _moduleManager.ShutdownModules();
            }
        }
    }
}
