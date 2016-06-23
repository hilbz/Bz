using Bz.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Runtime.Session
{
    /// <summary>
    /// 定义一些session信息可以用于application
    /// </summary>
    public interface IBzSession
    {
        /// <summary>
        /// 获取当前的UserId Or Null
        /// </summary>
        long? UserId { get; }

        /// <summary>
        /// 获取当前TenantId Or null
        /// </summary>
        int? TenantId { get; }

        /// <summary>
        /// 获取当前multi-tenancy side
        /// </summary>
        MultiTenancySides MultiTenancySide { get; }

        /// <summary>
        /// 模拟的UserId
        /// </summary>
        long? ImpersonatorUserId { get; }

        /// <summary>
        /// 模拟的租户Id
        /// 根据模拟的User获取的TenancyId<see cref="ImpersonatorUserId"/>
        /// </summary>
        int? ImpersonatorTenantId { get; }
    }
}
