using System;

namespace Bz.Web.Models
{
    /// <summary>
    /// 此类用于创建一个标准的AJAS相应结果
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    [Serializable]
    public class AjaxResponse<TResult>
    {
        /// <summary>
        /// Indicates success status of the result.
        /// Set <see cref="Error"/> if this value is false.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The actual result object of ajax request.
        /// It is set if <see cref="Success"/> is true.
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// 错误详情
        /// </summary>
        public ErrorInfo Error { get; set; }

        /// <summary>
        /// 是否没有权限访问
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }

        public AjaxResponse(TResult result)
        {
            Result = result;
            Success = true;
        }

        public AjaxResponse()
        {
            Success = true;
        }

        public AjaxResponse(bool success)
        {
            Success = success;
        }
        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }
    }
}
