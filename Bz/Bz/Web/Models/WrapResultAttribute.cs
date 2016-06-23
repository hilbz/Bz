using System;

namespace Bz.Web.Models
{
    /// <summary>
    /// 在Web Layer决定如何用Bz系统进行返回Response的包装
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class WrapResultAttribute:Attribute
    {
        /// <summary>
        /// Gets default <see cref="WrapResultAttribute"/>.
        /// </summary>
        public static WrapResultAttribute Default { get { return _default; } }
        private static readonly WrapResultAttribute _default = new WrapResultAttribute();

        /// <summary>
        /// 成功时的结果包装.
        /// </summary>
        public bool WrapOnSuccess { get; set; }

        /// <summary>
        /// 错误时候结果包装.
        /// </summary>
        public bool WrapOnError { get; set; }

        /// <summary>
        /// 是否输出日记错误.
        /// Default: true.
        /// </summary>
        public bool LogError { get; set; }

        public WrapResultAttribute(bool wrapOnSuccess = true, bool wrapOnError = true)
        {
            WrapOnSuccess = wrapOnSuccess;
            WrapOnError = wrapOnError;

            LogError = true;
        }
    }
}
