using Bz.Dapper.Configs;
using Bz.DapperWrapper;
using Bz.Modules;
using Castle.MicroKernel.Registration;
using System.Configuration;
using System.Reflection;

namespace Bz.Dapper
{
    [DependsOn(typeof(BzKernelModule))]
    public class BzDapperModule : BzModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.IocContainer.Register(
                Component.For<IDbExecutorFactory, SqlExecutorFactory>()
                    .UsingFactoryMethod(
                       () =>
                          new SqlExecutorFactory(
                              ConfigurationManager.ConnectionStrings[DbConfiguration.GetConnectionName(DbConfigType.Default)].ToString())
                              ).LifestyleTransient());
        }
    }
}