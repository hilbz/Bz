using Bz.Domain.Uow;
using Bz.Logging;
using Bz.Reflection;
using Bz.Runtime.Session;
using Bz.Web.Models;
using Bz.Web.Mvc.Models;
using Bz.Web.Mvc.Web.Controllers.Results;
using Castle.Core.Logging;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bz.Web.Mvc.Controllers
{
    /// <summary>
    /// Mvc Controller的基类in Bz System。
    /// </summary>
    public abstract class BzController : Controller
    {
        /// <summary>
        /// 获取 current session information.
        /// </summary>
        public IBzSession BzSession { get; set; }

        /// <summary>
        /// 引用the logger 用于写日记.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 工作单元 to <see cref="IUnitOfWorkManager"/>.
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new BzException("在使用工作单元前必须为其赋值.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 获取当前的工作单元.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        /// <summary>
        /// 显示一个action实际的执行时间
        /// </summary>
        private Stopwatch _actionStopwatch;

        /// <summary>
        /// 其实就是当前执行action的methodInfo.
        /// </summary>
        private MethodInfo _currentMethodInfo;

        /// <summary>
        /// 当前正在执行的action的包装WrapResultAttribute.
        /// </summary>
        private WrapResultAttribute _wrapResultAttribute;

        /// <summary>
        /// 忽略序列的Type
        /// </summary>
        private static Type[] _ignoredTypesForSerialization = { typeof(HttpPostedFileBase) };

        protected BzController()
        {
            BzSession = NullBzSession.Instance;
            Logger = NullLogger.Instance;
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            if (_wrapResultAttribute != null && !_wrapResultAttribute.WrapOnSuccess)
            {
                return base.Json(data, contentType, contentEncoding, behavior);
            }

            if (data == null)
            {
                data = new AjaxResponse();
            }
            else if (!ReflectionHelper.IsAssignableToGenericType(data.GetType(), typeof(AjaxResponse<>)))
            {
                data = new AjaxResponse(data);
            }

            return new BzJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        #region OnActionExecuting/OnActionExecuted
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetCurrentMethodInfoAndWrapResultAttribute(filterContext);
            base.OnActionExecuting(filterContext);
        }

        private void SetCurrentMethodInfoAndWrapResultAttribute(ActionExecutingContext filterContext)
        {
            _currentMethodInfo = ActionDescriptorHelper.GetMethodInfo(filterContext.ActionDescriptor);
            _wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrNull<WrapResultAttribute>(_currentMethodInfo) ??
                WrapResultAttribute.Default;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        #endregion

        #region Exception handling

        protected override void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            //如果错误已被处理，那啥都不做.
            //If this is child action, exception should be handled by main action.
            if (context.ExceptionHandled || context.IsChildAction)
            {
                base.OnException(context);
                return;
            }
            //记录错误日记
            if (_wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            if (!context.HttpContext.IsCustomErrorEnabled)
            {
                base.OnException(context);
                return;
            }
            //如果不是一个500错误，可能是404之类的
            if (new HttpException(null,context.Exception).GetHashCode()!=500)
            {
                base.OnException(context);
                return;
            }
            //检查WrapResultAttribute是否设置包裹错误
            if (!_wrapResultAttribute.WrapOnError)
            {
                base.OnException(context);
                return;
            }

            //标志成已经处理异常
            context.ExceptionHandled = true;

            //返回一个特殊的错误到客户端
            context.HttpContext.Response.Clear();

            context.Result = IsJsonResult()
                ? GenerateJsonExceptionResult(context)
                : GenerateNonJsonExceptionResult(context);

            //可能IIS会只有自己错误的页面
            context.HttpContext.Response.TrySkipIisCustomErrors = true;

            //触发一个错误的事件

        }

        protected virtual bool IsJsonResult()
        {
            return typeof(JsonResult).IsAssignableFrom(_currentMethodInfo.ReturnType) ||
                   typeof(Task<JsonResult>).IsAssignableFrom(_currentMethodInfo.ReturnType);
        }

        protected virtual ActionResult GenerateJsonExceptionResult(ExceptionContext context)
        {
            //TODO:权限验证的错误判断
            context.HttpContext.Response.StatusCode = 200; //TODO: Consider to return 500
            return new BzJsonResult(
                new MvcAjaxResponse(
                    ErrorInfoBuilder.Instance.BuildForException(context.Exception),
                    false
                    )
                );
        }

        protected virtual ActionResult GenerateNonJsonExceptionResult(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 500;
            return new ViewResult
            {
                ViewName = "Error",
                MasterName = string.Empty,
                ViewData = new ViewDataDictionary<ErrorViewModel>(new ErrorViewModel(context.Exception)),
                TempData = context.Controller.TempData
            };
        }

        #endregion
    }
}
