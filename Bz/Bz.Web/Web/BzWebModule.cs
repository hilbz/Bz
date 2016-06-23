using Bz.Modules;
using Bz.Web.Configuration;
using System.Reflection;

namespace Bz.Web
{
    /// <summary>
    /// 该模块用于Bz System In Asp.NET Web application
    /// </summary>
    [DependsOn(typeof(BzKernelModule))]
    public class BzWebModule:BzModule
    {

        public override void PreInitialize()
        {
            IocManager.Register<IBzWebModuleConfiguration, BzWebModuleConfiguration>();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
