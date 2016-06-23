using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace Bz.Web.Mvc.Controllers
{
    /// <summary>
    /// 顾名思义吧
    /// </summary>
    public static class ActionDescriptorHelper
    {
        public static MethodInfo GetMethodInfo(ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ReflectedActionDescriptor)
            {
                return ((ReflectedActionDescriptor)actionDescriptor).MethodInfo;
            }

            if (actionDescriptor is ReflectedAsyncActionDescriptor)
            {
                return ((ReflectedAsyncActionDescriptor)actionDescriptor).MethodInfo;
            }

            if (actionDescriptor is TaskAsyncActionDescriptor)
            {
                return ((TaskAsyncActionDescriptor)actionDescriptor).MethodInfo;
            }
            throw new BzException("获取不到 MethodInfo for the action '" + actionDescriptor.ActionName + "' of controller '" + actionDescriptor.ControllerDescriptor.ControllerName + "'.");

        }
    }
}
