using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bz
{
    /// <summary>
    /// 由Bz System抛出的错误或则特殊错误的基类
    /// </summary>
    [Serializable]
    public class BzException:Exception
    {
        /// <summary>
        /// 创建一个新的<see cref="BzException"/>对象
        /// </summary>
        public BzException()
        {

        }

        /// <summary>
        /// 创建一个新的<see cref="BzException"/>对象
        /// </summary>
        public BzException(SerializationInfo serializationInfo, StreamingContext context)
            :base(serializationInfo,context)
        {

        }

        /// <summary>
        /// 创建一个新的<see cref="BzException"/>对象
        /// </summary>
        /// <param name="message">错误的信息</param>
        public BzException(string message)
            :base(message)
        {

        }

        /// <summary>
        /// 创建一个新的<see cref="BzException"/>对象
        /// </summary>
        /// <param name="message">错误的信息</param>
        /// <param name="innerException">内部错误信息</param>
        public BzException(string message, Exception innerException)
            :base(message,innerException)
        {

        }
    }
}
