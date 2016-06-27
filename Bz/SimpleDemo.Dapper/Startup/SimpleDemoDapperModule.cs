using Bz;
using Bz.Modules;
using System.Reflection;

namespace SimpleDemo.Dapper
{
    [DependsOn(typeof(BzKernelModule))]
    public class SimpleDemoDapperModule : BzModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());            
        }
    }
}
