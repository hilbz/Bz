using Bz.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Runtime.Session
{
    /// <summary>
    /// 当没有匹配时<see cref="IBzSession"/>
    /// </summary>
    public class NullBzSession:IBzSession
    {
        /// <summary>
        /// 单例.
        /// </summary>
        public static NullBzSession Instance { get { return SingletonInstance; } }
        private static readonly NullBzSession SingletonInstance = new NullBzSession();

        /// <inheritdoc/>
        public long? UserId { get { return null; } }

        /// <inheritdoc/>
        public int? TenantId { get { return null; } }

        public MultiTenancySides MultiTenancySide { get { return MultiTenancySides.Tenant; } }

        public long? ImpersonatorUserId { get { return null; } }

        public int? ImpersonatorTenantId { get { return null; } }

        private NullBzSession()
        {

        }
    }
}
