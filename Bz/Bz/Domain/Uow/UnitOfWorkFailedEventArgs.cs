using System;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 作为一个事件参数
    /// </summary>
    public class UnitOfWorkFailedEventArgs:EventArgs
    {

        /// <summary>
        /// 执行失败时触发
        /// </summary>
        public Exception Exception { get;private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public UnitOfWorkFailedEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
