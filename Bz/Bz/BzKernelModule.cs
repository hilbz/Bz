using Bz.Auditing;
using Bz.Dependency;
using Bz.Domain.Uow;
using Bz.Modules;
using Bz.MultiTenancy;
using Bz.Runtime.Session;
using Bz.Runtime.Validation.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz
{
    /// <summary>
    /// Bz系统中的核心模块
    /// 不用继承此类，会自动作为系统中第一个启动的模块
    /// </summary>
    public sealed class BzKernelModule : BzModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());
            ValidationInterceptorRegistrar.Initialize(IocManager);
            //AuditingInterceptorRegistrar.Initialize(IocManager);
            UnitOfWorkRegistrar.Initialize(IocManager);
        }

        public override void Initialize()
        {
            base.Initialize();
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(),
                 new ConventionalRegistrationConfig
                 {
                     InstallInstallers = false
                 });
        }

        public override void PostInitialize()
        {
            RegisterMissingComponents();
            base.PostInitialize();
        }
        public override void Shutdown()
        {
            base.Shutdown();
        }

        private void RegisterMissingComponents()
        {
            IocManager.RegisterIfNot<IGuidGenerator, SequentialGuidGenerator>(DependencyLifeStyle.Transient);
            IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);
            IocManager.RegisterIfNot<ITenantIdResolver, NullTenantIdResolver>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IBzSession, ClaimsBzSession>(DependencyLifeStyle.Singleton);

        }
    }
}
