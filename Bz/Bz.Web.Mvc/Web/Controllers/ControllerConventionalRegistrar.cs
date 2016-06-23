using Bz.Dependency;
using Castle.MicroKernel.Registration;
using System.Web.Mvc;

namespace Bz.Web.Mvc.Controllers
{
    /// <summary>
    /// 注册所有的Mvc Controller 继承于<see cref="Controller"/>
    /// </summary>
    public class ControllerConventionalRegistrar : IConventionalDependencyRegistrar
    {
        /// <inheritdoc/>
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .BasedOn<Controller>()
                    .LifestyleTransient()
                );
        }
    }
}
