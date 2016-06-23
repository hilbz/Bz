namespace Bz.Domain.Entities
{
    /// <summary>
    /// 实现该接口的实体，对于TenantId是可选的
    /// </summary>
    public interface IMayHaveTenant
    {
        /// <summary>
        /// 该实体的TenantId
        /// </summary>
        int? TenantId { get; set; }
    }
}
