using Bz.Threading;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Owin
{
    /// <summary>
    /// OWIN的扩展方法For BzSystem
    /// </summary>
    public static class BzOwinExtensions
    {
        /// <summary>
        /// 使用Bz
        /// </summary>
        /// <param name="app"></param>
        public static void UseBz(this IAppBuilder app)
        {
            ThreadCultureSanitizer.Sanitize();
        }
    }
}
