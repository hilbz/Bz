using Bz.Configuration.Startup;
using Bz.Modules;
using Bz.Web;
using Bz.WebApi.Configuraion;
using Bz.WebApi.Controllers;
using Bz.WebApi.Controllers.Filters;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;

namespace Bz.WebApi
{
    [DependsOn(typeof(BzWebModule))]
    public class BzWebApiModule:BzModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());
            IocManager.Register<IBzWebApiModuleConfiguration, BzWebApiModuleConfiguration>();
            
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            var httpConfiguration = IocManager.Resolve<IBzWebApiModuleConfiguration>().HttpConfiguration;

            InitializeFilters(httpConfiguration);
            InitializeFormatters(httpConfiguration);
            InitializeRoutes(httpConfiguration);
        }

        public override void PostInitialize()
        {
            Configuration.Modules.BzWebApi().HttpConfiguration.EnsureInitialized();
        }
        private void InitializeFilters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.MessageHandlers.Add(IocManager.Resolve<ResultWrapperHandler>());
            httpConfiguration.Filters.Add(IocManager.Resolve<BzExceptionFilterAttribute>());
        }

        private static void InitializeFormatters(HttpConfiguration httpConfiguration)
        {
            //Remove formatters except JsonFormatter.
            foreach (var currentFormatter in httpConfiguration.Formatters.ToList())
            {
                if (!(currentFormatter is JsonMediaTypeFormatter))
                {
                    httpConfiguration.Formatters.Remove(currentFormatter);
                }
            }

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //httpConfiguration.Formatters.Add(new PlainTextFormatter());
        }
        private static void InitializeRoutes(HttpConfiguration httpConfiguration)
        {
            //Dynamic Web APIs
            httpConfiguration.Routes.MapHttpRoute(
                name: "AbpDynamicWebApi",
                routeTemplate: "api/services/{*serviceNameWithAction}"
                );

            //Other routes
            httpConfiguration.Routes.MapHttpRoute(
                name: "AbpCacheController_Clear",
                routeTemplate: "api/AbpCache/Clear",
                defaults: new { controller = "AbpCache", action = "Clear" }
                );

            httpConfiguration.Routes.MapHttpRoute(
                name: "AbpCacheController_ClearAll",
                routeTemplate: "api/AbpCache/ClearAll",
                defaults: new { controller = "AbpCache", action = "ClearAll" }
                );
        }
    }
}
