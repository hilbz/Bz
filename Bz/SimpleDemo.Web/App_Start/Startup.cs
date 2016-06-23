using Bz.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleDemo.Web.Startup))]
namespace SimpleDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseBz();            
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}
