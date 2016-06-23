using Bz.Web.Configuration;

namespace Bz.Configuration.Startup
{
    public static class BzWebConfigurationExtensions
    {
        public static IBzWebModuleConfiguration BzWeb(this IModuleConfigurations configurations)
        {
            return configurations.BzConfiguration.GetOrCreate("Modules.Bz.Web", () => configurations.BzConfiguration.IocManager.Resolve<IBzWebModuleConfiguration>());
        }
    }
}
