﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Web.Models
{
    /// <summary>
    /// 用于存储错误的信息
    /// </summary>
    [Serializable]
    public class ErrorInfo
    {
        /// <summary>
        /// 错误码.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误详情.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误详情.
        /// </summary>
        public string Details { get; set; }


        /// <summary>
        /// 如果存在错误的结果集.
        /// </summary>
        public ValidationErrorInfo[] ValidationErrors { get; set; }

        public ErrorInfo()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="ErrorInfo"/>.
        /// </summary>
        /// <param name="message">Error message</param>
        public ErrorInfo(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ErrorInfo"/>.
        /// </summary>
        /// <param name="code">Error code</param>
        public ErrorInfo(int code)
        {
            Code = code;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ErrorInfo"/>.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        public ErrorInfo(int code, string message)
            : this(message)
        {
            Code = code;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ErrorInfo"/>.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="details">Error details</param>
        public ErrorInfo(string message, string details)
            : this(message)
        {
            Details = details;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ErrorInfo"/>.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        /// <param name="details">Error details</param>
        public ErrorInfo(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }
    }
}
