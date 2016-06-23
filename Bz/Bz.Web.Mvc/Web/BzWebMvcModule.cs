using Bz.Modules;
using Bz.Web.Mvc.Controllers;
using System.Reflection;
using System.Web.Mvc;

namespace Bz.Web.Mvc
{

    /// <summary>
    /// 该模块用于构建 ASP.NET MVC web sites using Bz.
    /// </summary>
    [DependsOn(typeof(BzWebModule))]
    public class BzWebMvcModule:BzModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar());
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager));
        }
    }
}
