using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Runtime.Session
{
    /// <summary>
    /// 扩展方法<see cref="IBzSession"/>
    /// </summary>
    public static class BzSessionExtensions
    {
        /// <summary>
        /// 获取 current User's Id.
        /// Throws <see cref="BzException"/> if <see cref="IAbpSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current User's Id.</returns>
        public static long GetUserId(this IBzSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new BzException("Session.UserId 为空!有可能用户未登录.");
            }

            return session.UserId.Value;
        }

        /// <summary>
        /// 获取 current Tenant's Id.
        /// Throws <see cref="BzException"/> if <see cref="IAbpSession.TenantId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current Tenant's Id.</returns>
        /// <exception cref="BzException"></exception>
        public static int GetTenantId(this IBzSession session)
        {
            if (!session.TenantId.HasValue)
            {
                throw new BzException("站点Id为空!为用户未登录，或则以HostUser方式登录((TenantId is always null for host users)).");
            }

            return session.TenantId.Value;
        }
    }
}
