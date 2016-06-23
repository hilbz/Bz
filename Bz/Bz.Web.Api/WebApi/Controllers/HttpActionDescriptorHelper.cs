using Bz.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;

namespace Bz.WebApi.Controllers
{
    internal static class HttpActionDescriptorHelper
    {
        public static WrapResultAttribute GetWrapResultAttributeOrNull(HttpActionDescriptor actionDescriptor)
        {
            if (actionDescriptor == null)
            {
                return null;
            }

            var wrapAttr = actionDescriptor.Properties.GetOrDefault("__BzDynamicApiDontWrapResultAttribute") as WrapResultAttribute;
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            wrapAttr = actionDescriptor.GetCustomAttributes<WrapResultAttribute>().FirstOrDefault();
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            wrapAttr = actionDescriptor.ControllerDescriptor.GetCustomAttributes<WrapResultAttribute>().FirstOrDefault();
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            //Not found
            return null;
        }
    }
}
