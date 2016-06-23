using Bz.Modules;
using SimpleDemo.Core;
using System.Reflection;

namespace SimpleDemo.Application
{
    [DependsOn(typeof(SimpleDemoCoreModule))]
    public class SimpleDemoApplicationModule:BzModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
