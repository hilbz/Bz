using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Logging
{
    /// <summary>
    /// 定义一个日记的级别<see cref="LogSeverity"/>
    /// </summary>   
    public interface IHasLogSeverity
    {
        /// <summary>
        /// 日记级别
        /// </summary>
        LogSeverity Severity { get; set; }
    }
}
