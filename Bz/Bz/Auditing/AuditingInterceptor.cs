using Bz.Runtime.Session;
using Castle.Core.Logging;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Auditing
{
    internal class AuditingInterceptor:IInterceptor
    {
        public IBzSession BzSession { get; set; }

        public ILogger Logger { get; set; }

        public void Intercept(IInvocation invocation)
        {

        }
    }
}
