using Bz.Web.Models;
using System;

namespace Bz.Web.Mvc.Models
{
    /// <summary>
    /// 创建一个标准的Ajax请求返回对象
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    [Serializable]
    public class MvcAjaxResponse<TResult> : AjaxResponse<TResult>
    {
        /// <summary>
        /// 跳转到特殊的链接
        /// </summary>
        public string TargetUrl { get; set; }

        public MvcAjaxResponse()
        {

        }

        public MvcAjaxResponse(bool success)
            : base(success)
        {

        }

        public MvcAjaxResponse(TResult result)
            : base(result)
        {

        }

        public MvcAjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }
}
