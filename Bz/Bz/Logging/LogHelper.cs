using Bz.Dependency;
using Bz.Runtime.Validation;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Logging
{
    /// <summary>
    /// 此类用于写日记，必须有个引用<see cref="ILogger"/>
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 一个Logger的引用
        /// </summary>
        public static ILogger Logger { get; set; }

        static LogHelper()
        {
            Logger = IocManager.Instance.IsRegistered(typeof(ILoggerFactory))
                ? IocManager.Instance.Resolve<ILoggerFactory>().Create(typeof(LogHelper))
                : NullLogger.Instance;
        }

        public static void LogException(Exception ex)
        {
            LogException(Logger, ex);
        }

        public static void LogException(ILogger logger, Exception ex)
        {
            var severity = (ex is IHasLogSeverity)
                ? (ex as IHasLogSeverity).Severity
                : LogSeverity.Error;

            logger.Log(severity, ex.Message, ex);

            LogValidationErrors(logger, ex);

        }

        private static void LogValidationErrors(ILogger logger, Exception exception)
        {
            //尝试记录内部验证错误信息
            if (exception is AggregateException &&exception.InnerException!=null)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerException is BzValidationException)
                {
                    exception = aggException.InnerException;
                }
            }

            if (!(exception is BzValidationException))
            {
                return;
            }

            var validationExcepiton = exception as BzValidationException;
            if (validationExcepiton.ValidationErrors.IsNullOrEmpty())
            {
                return;
            }

            logger.Log(validationExcepiton.Severity, "一共有" + validationExcepiton.ValidationErrors.Count+"验证错误。");
            foreach (var validationResult in validationExcepiton.ValidationErrors)
            {
                var memberNames = "";
                if (validationResult.MemberNames!=null&&validationResult.MemberNames.Any())
                {
                    memberNames = "(" + string.Join(",", validationResult.MemberNames) + ")";
                }
                logger.Log(validationExcepiton.Severity, validationResult.ErrorMessage + memberNames);
            }

        }
    }
}
