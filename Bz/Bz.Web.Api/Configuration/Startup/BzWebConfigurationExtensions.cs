using Bz.WebApi.Configuraion;

namespace Bz.Configuration.Startup
{
    /// <summary>
    /// 扩展方法 <see cref="IModuleConfigurations"/>
    /// </summary>
    public static class BzWebApiConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Bz.Web.Api module.
        /// </summary>
        public static IBzWebApiModuleConfiguration BzWebApi(this IModuleConfigurations configurations)
        {
            return configurations.BzConfiguration.GetOrCreate("Modules.Bz.Web.Api", () => configurations.BzConfiguration.IocManager.Resolve<IBzWebApiModuleConfiguration>());
        }
    }
}
