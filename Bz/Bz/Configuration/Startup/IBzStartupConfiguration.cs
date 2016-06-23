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
    /// 处理Bz and Moudles 在启动时的配置
    /// </summary>
    public interface IBzStartupConfiguration:IDictionaryBasedConfig
    {
        IIocManager IocManager { get; }

        /// <summary>
        /// 在ORM模块获取和设置默认数据库连接.
        /// 可以是个别名或则是全部配置[ContectionString=..,UserId=..,Pwd=...].
        /// </summary>
        string DefaultNameOrConnectionString { get; set; }

        /// <summary>
        /// 用于配置模块.
        ///为一些特殊模块添加一下模块配置 <see cref="IModuleConfigurations"/>.
        /// </summary>
        IModuleConfigurations Modules { get; }

        /// <summary>
        /// 用于配置一个多站点.
        /// </summary>
        IMultiTenancyConfig MultiTenancy { get; }

        /// <summary>
        /// 工作单元默认配置.
        /// </summary>
        IUnitOfWorkDefaultOptions UnitOfWork { get; }


    }
}
