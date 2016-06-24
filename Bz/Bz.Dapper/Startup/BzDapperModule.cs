using Bz.Modules;
using System.Reflection;

namespace Bz.Dapper
{
    [DependsOn(typeof(BzKernelModule))]
    public class BzDapperModule:BzModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //IocManager.IocContainer 张婷婷
        }
    }
}
