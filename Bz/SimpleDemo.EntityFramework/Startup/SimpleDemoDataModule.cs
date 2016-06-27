using Bz;
using Bz.EntityFramework;
using Bz.Modules;
using SimpleDemo.Core;
using System.Data.Entity;
using System.Reflection;

namespace SimpleDemo.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(BzKernelModule),
        typeof(BzEntityFrameworkModule), 
        typeof(SimpleDemoCoreModule))]
    public class SimpleDemoDataModule:BzModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<SimpleDemoDbContext>(null);
        }
    }
}
