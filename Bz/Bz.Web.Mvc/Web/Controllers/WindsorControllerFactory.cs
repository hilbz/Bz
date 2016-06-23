using Bz.Dependency;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bz.Web.Mvc.Controllers
{
    /// <summary>
    /// 此类用于允许MVC使用DI 在创建Mvc Controller的时候
    /// </summary>
    public class WindsorControllerFactory: DefaultControllerFactory
    {
        /// <summary>
        /// 引用DI核心注册.
        /// </summary>
        private readonly IIocResolver _iocManager;

        public WindsorControllerFactory(IIocResolver iocManager)
        {
            _iocManager = iocManager;
        }

        /// <summary>
        /// 当给定的Controller释放的时候出发
        /// </summary>
        /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            _iocManager.Release(controller);
        }

        /// <summary>
        /// 创建controller实例的时候触发.
        /// </summary>
        /// <param name="requestContext">Request context</param>
        /// <param name="controllerType">Controller type</param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }

            return _iocManager.Resolve<IController>(controllerType);
        }
    }
}
