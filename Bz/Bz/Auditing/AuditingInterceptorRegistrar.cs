using Bz.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Auditing
{
    internal static class AuditingInterceptorRegistrar
    {
        private static IAuditingConfiguration _auditingConfiguration;

        public static void Initialize(IIocManager iocManager)
        {
            _auditingConfiguration = iocManager.Resolve<IAuditingConfiguration>();

            if (!_auditingConfiguration.IsEnabled)
            {
                return;
            }

            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            if (ShouldIntercept(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new Castle.Core.InterceptorReference(typeof(AuditingInterceptor)));
            }
        }

        private static bool ShouldIntercept(Type type)
        {
            if (_auditingConfiguration.Selectors.Any(selector=>selector.Predicate(type)))
            {
                return true;
            }
            //TODO:true or false?
            if (type.IsDefined(typeof(AuditedAttribute),true))
            {
                return true;
            }

            //TODO:true or false?
            if (type.GetMethods().Any(m=>m.IsDefined(typeof(AuditedAttribute),true)))
            {
                return true;
            }

            return false;
        }
    }
}
