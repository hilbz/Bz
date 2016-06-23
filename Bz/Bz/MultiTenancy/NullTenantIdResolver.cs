using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.MultiTenancy
{
    /// <summary>
    /// 作为无站点方式的一种实现<see cref="ITenantIdResolver"/>
    /// </summary>
    public class NullTenantIdResolver:ITenantIdResolver
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static NullTenantIdResolver Instance { get { return SingletonInstance; } }

        private static readonly NullTenantIdResolver SingletonInstance = new NullTenantIdResolver();

        public int? TenantId { get { return null; } }
    }
}
