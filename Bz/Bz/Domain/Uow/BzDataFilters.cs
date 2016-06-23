using System;
using System.Collections.Generic;
using System.Linq;
using Bz.Domain.Entities;


namespace Bz.Domain.Uow
{
    /// <summary>
    /// BZ 系统的过滤器
    /// </summary>
    public static class BzDataFilters
    {
        /// <summary>
        /// 软删除过滤器
        /// <see cref="ISoftDelete"/>
        /// </summary>
        public const string SoftDelete = "SoftDelete";

        /// <summary>
        /// 多租户过滤器
        /// </summary>
        public const string MustHaveTenant = "MustHaveTenant";

        /// <summary>
        /// 多租户过滤器
        /// </summary>
        public const string MayHaveTenant = "MayHaveTenant";

        /// <summary>
        /// 标准化参数Of Bz
        /// </summary>
        public static class Parameters
        {
            /// <summary>
            /// "tenantId".
            /// </summary>
            public const string TenantId = "tenantId";
        }
    }
}
