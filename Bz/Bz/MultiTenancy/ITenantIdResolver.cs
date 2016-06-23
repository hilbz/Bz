using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.MultiTenancy
{
    /// <summary>
    /// 用于获取当前的站点Id
    /// 该接口将会实现获取TenantId在User未登录的情况
    /// 当然也可以通过subDomain来获取TenantId,for example
    /// </summary>
    public interface ITenantIdResolver
    {
        /// <summary>
        /// 获取当前TenantId or null.
        /// </summary>
        int? TenantId { get; }
    }
}
