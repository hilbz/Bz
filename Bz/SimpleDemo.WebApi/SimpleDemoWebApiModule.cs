using Bz.Modules;
using Bz.WebApi;
using SimpleDemo.Application;
using System.Reflection;

namespace SimpleDemo.WebApi
{
    [DependsOn(typeof(BzWebApiModule),typeof(SimpleDemoApplicationModule))]
    public class SimpleDemoWebApiModule:BzModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
