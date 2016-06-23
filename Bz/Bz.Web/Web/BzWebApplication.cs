using Bz.Reflection;
using Bz.Threading;
using System;
using System.Web;
using Bz.Dependency;
using Bz.MultiTenancy;
using System.Security.Claims;
using System.Linq;
using Bz.Runtime.Security;
using System.Globalization;

namespace Bz.Web
{
    /// <summary>
    /// 此类用于启动Bz系统
    /// 需要再global.asax重写<see cref="HttpApplication"/>
    /// </summary>
    public abstract class BzWebApplication : HttpApplication
    {
        /// <summary>
        /// Gets a reference to the <see cref="AbpBootstrapper"/> instance.
        /// </summary>
        protected BzBootstrapper BzBootstrapper { get; private set; }
        protected BzWebApplication()
        {
            BzBootstrapper = new BzBootstrapper();
        }

        /// <summary>
        /// This method is called by ASP.NET system on web application's startup.
        /// </summary>
        protected virtual void Application_Start(object sender, EventArgs e)
        {
            ThreadCultureSanitizer.Sanitize();

            BzBootstrapper.IocManager.RegisterIfNot<IAssemblyFinder, WebAssemblyFinder>();
            BzBootstrapper.Initialize();
        }

        protected virtual void Application_End(object sender, EventArgs e)
        {
            BzBootstrapper.Dispose();
        }

        /// <summary>
        /// This method is called by ASP.NET system when a session starts.
        /// </summary>
        protected virtual void Session_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is called by ASP.NET system when a session ends.
        /// </summary>
        protected virtual void Session_End(object sender, EventArgs e)
        {

        }
        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// This method is called by ASP.NET system when a request ends.
        /// </summary>
        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            TrySetTenantId();
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Tries to set current tenant Id.
        /// </summary>
        protected virtual void TrySetTenantId()
        {
            var claimsPrincipal = User as ClaimsPrincipal;
            if (claimsPrincipal == null)
            {
                return;
            }

            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
            {
                return;
            }

            var tenantIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == BzClaimTypes.TenantId);
            if (tenantIdClaim != null)
            {
                return;
            }

            var tenantId = ResolveTenantIdOrNull();
            if (!tenantId.HasValue)
            {
                return;
            }

            claimsIdentity.AddClaim(new Claim(BzClaimTypes.TenantId, tenantId.Value.ToString(CultureInfo.InvariantCulture)));
        }
        /// <summary>
        /// Resolves current tenant id or returns null if can not.
        /// </summary>
        protected virtual int? ResolveTenantIdOrNull()
        {
            using (var tenantIdResolver = BzBootstrapper.IocManager.ResolveAsDisposable<ITenantIdResolver>())
            {
                return tenantIdResolver.Object.TenantId;
            }
        }
    }
}
