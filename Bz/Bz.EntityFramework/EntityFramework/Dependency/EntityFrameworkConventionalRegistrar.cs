using Bz.Configuration.Startup;
using Bz.Dependency;
using Bz.EntityFramework;
using Castle.MicroKernel.Registration;
using System.Configuration;

namespace Bz.EntityFramework.Dependency
{
    /// <summary>
    /// 通过配置注册继承于BzContext的Context
    /// </summary>
    public class EntityFrameworkConventionalRegistrar: IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<BzDbContext>()
                    .WithServiceSelf()
                    .LifestyleTransient()
                    .Configure(c=>c.DynamicParameters(
                        (kernel,dynamicParams)=>
                        {
                            var connectionString = GetNameOrConnectionStringOrNull(context.IocManager);
                            if (!string.IsNullOrWhiteSpace(connectionString))
                            {
                                dynamicParams["nameOrConnectionString"] = connectionString;
                            }
                        })));
        }

        private static string GetNameOrConnectionStringOrNull(IIocResolver iocResolver)
        {
            if (iocResolver.IsRegistered<IBzStartupConfiguration>())
            {
                var defaultConnectionString = iocResolver.Resolve<IBzStartupConfiguration>().DefaultNameOrConnectionString;
                if (!string.IsNullOrWhiteSpace(defaultConnectionString))
                {
                    return defaultConnectionString;
                }
            }
            if (ConfigurationManager.ConnectionStrings["Default"] != null)
            {
                return "Default";
            }

            return null;
        }
    }
}
