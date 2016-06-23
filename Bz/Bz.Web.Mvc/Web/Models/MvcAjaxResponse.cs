using Bz.Web.Models;
using System;

namespace Bz.Web.Mvc.Models
{
    /// <summary>
    /// 创建一个标准的Ajax请求返回对象
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    [Serializable]
    public class MvcAjaxResponse:MvcAjaxResponse<object>
    {
        public MvcAjaxResponse()
        {

        }

        public MvcAjaxResponse(bool success)
            : base(success)
        {

        }

        public MvcAjaxResponse(object result)
            : base(result)
        {

        }

        public MvcAjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }
}
