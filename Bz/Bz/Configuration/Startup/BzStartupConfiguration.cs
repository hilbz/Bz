using Bz.Dependency;
using Bz.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Configuration.Startup
{
    /// <summary>
    /// 对Bz System和模块在启动时进行配置
    /// </summary>
    internal class BzStartupConfiguration:DictionaryBasedConfig,IBzStartupConfiguration
    {
        public IIocManager IocManager { get; set; }

        /// <summary>
        /// 在ORM模块获取和设置默认数据库连接.
        /// 可以是个别名或则是全部配置[ContectionString=..,UserId=..,Pwd=...].
        /// </summary>
        public string DefaultNameOrConnectionString { get; set; }

        /// <summary>
        /// 用于配置模块.
        ///为一些特殊模块添加一下模块配置 <see cref="IModuleConfigurations"/>.
        /// </summary>
        public IModuleConfigurations Modules { get; private set; }

        /// <summary>
        /// 工作单元默认配置.
        /// </summary>
        public IUnitOfWorkDefaultOptions UnitOfWork { get; private set; }


        /// <summary>
        /// 用于配置多租户.
        /// </summary>
        public IMultiTenancyConfig MultiTenancy { get; private set; }

        public void Initialize()
        {
            Modules = IocManager.Resolve<IModuleConfigurations>();
            UnitOfWork = IocManager.Resolve<IUnitOfWorkDefaultOptions>();
            MultiTenancy = IocManager.Resolve<IMultiTenancyConfig>();

        }
    }
}
