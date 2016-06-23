using Bz.Dependency;
using Bz.Reflection;
using Bz.Web;
using Castle.Facilities.Logging;
using System;

namespace SimpleDemo.Web
{
    public class MvcApplication : BzWebApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            BzBootstrapper.IocManager.RegisterIfNot<IAssemblyFinder, CurrentDomainAssemblyFinder>();

            BzBootstrapper.IocManager.IocContainer
                .AddFacility<LoggingFacility>(f => f.UseLog4Net()
                    .WithConfig("log4net.config")
                );

            base.Application_Start(sender, e);
        }
    }
}
