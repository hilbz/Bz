using Bz.Domain.Entities;

namespace Bz.MultiTenancy
{
    /// <summary>
    /// multi-tenancy的扩展方法
    /// </summary>
    public static class MultiTenancyExtensions
    {
        /// <summary>
        /// 获取一个实现于<see cref="IMayHaveTenant"/>的multi-tenancy side
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns></returns>
        public static MultiTenancySides GetMultiTenancySide(this IMayHaveTenant obj)
        {
            return obj.TenantId.HasValue
                ? MultiTenancySides.Tenant
                : MultiTenancySides.Host;
        }
    }
}
