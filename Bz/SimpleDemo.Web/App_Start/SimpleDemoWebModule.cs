using Bz.Modules;
using Bz.Web.Mvc;
using SimpleDemo.Application;
using SimpleDemo.EntityFramework;
using SimpleDemo.WebApi;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SimpleDemo.Web
{
    [DependsOn(
        typeof(BzWebMvcModule),
        typeof(SimpleDemoDataModule),
        typeof(SimpleDemoApplicationModule),
        typeof(SimpleDemoWebApiModule))]
    public class SimpleDemoWebModule:BzModule
    {
        public override void Initialize()
        {
            //Dependency Injection
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Areas
            AreaRegistration.RegisterAllAreas();

            //Routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Bundling
            BundleTable.Bundles.IgnoreList.Clear();
        }
    }
}