using Bz;
using Bz.Modules;
using System.Reflection;

namespace SimpleDemo.Core
{
    [DependsOn(typeof(BzKernelModule))]
    public class SimpleDemoCoreModule:BzModule
    {
        public override void PreInitialize()
        {
            //Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
