using Bz.Logging;
using System;
using System.Runtime.Serialization;

namespace Bz.UI
{
    /// <summary>
    /// 该错误直接返回给用户
    /// </summary>
    [Serializable]
    public class UserFriendlyException:BzException,IHasLogSeverity
    {
        /// <summary>
        /// 错误的细节.
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// 错误码.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 日记级别.
        /// Default: Warn.
        /// </summary>
        public LogSeverity Severity { get; set; }

        /// <summary>
        /// 构造函数.
        /// </summary>
        public UserFriendlyException()
        {
            Severity = LogSeverity.Warn;
        }
        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public UserFriendlyException(string message)
            : base(message)
        {
            Severity = LogSeverity.Warn;
        }

        public UserFriendlyException(string message, LogSeverity severity)
            : base(message)
        {
            Severity = severity;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="message">异常错误信息</param>
        public UserFriendlyException(int code, string message)
            : this(message)
        {
            Code = code;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="message">异常错误信息</param>
        /// <param name="details">附加的错误信息</param>
        public UserFriendlyException(string message, string details)
            : this(message)
        {
            Details = details;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="message">异常错误信息</param>
        /// <param name="details">附加的错误信息</param>
        public UserFriendlyException(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="message">异常错误信息</param>
        /// <param name="innerException">内部错误</param>
        public UserFriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="message">异常错误信息</param>
        /// <param name="details">附加的错误信息</param>
        /// <param name="innerException">内部错误</param>
        public UserFriendlyException(string message, string details, Exception innerException)
            : this(message, innerException)
        {
            Details = details;
        }
    }
}
