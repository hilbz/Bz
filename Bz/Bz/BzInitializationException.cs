using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bz
{
    /// <summary>
    /// 在Bz initialization 进度的时候抛出捕获
    /// </summary>
    [Serializable]
    public class BzInitializationException : BzException
    {
        public BzInitializationException()
        {

        }

        public BzInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public BzInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">内部错误</param>
        public BzInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
