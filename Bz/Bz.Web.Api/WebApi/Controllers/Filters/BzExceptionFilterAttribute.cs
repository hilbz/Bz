using Bz.Dependency;
using Castle.Core.Logging;
using System.Web.Http.Filters;

namespace Bz.WebApi.Controllers.Filters
{
    /// <summary>
    /// 处理WebAPI Controllers层的错误
    /// </summary>
    public class BzExceptionFilterAttribute: ExceptionFilterAttribute,ITransientDependency
    {
        /// <summary>
        /// Reference to the <see cref="ILogger"/>.
        /// </summary>
        public ILogger Logger { get; set; }

        public BzExceptionFilterAttribute()
        {
            Logger = NullLogger.Instance;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
        }
    }
}
