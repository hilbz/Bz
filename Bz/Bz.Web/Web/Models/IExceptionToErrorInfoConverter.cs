using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Web.Models
{
    /// <summary>
    /// 将错误转化为ErrorInfo
    /// </summary>
    public interface IExceptionToErrorInfoConverter
    {
        /// <summary>
        /// Next converter. If this converter decide this exception is not known, it can call Next.Convert(...).
        /// </summary>
        IExceptionToErrorInfoConverter Next { set; }

        /// <summary>
        /// Converter method.
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns>Error info or null</returns>
        ErrorInfo Convert(Exception exception);
    }
}
