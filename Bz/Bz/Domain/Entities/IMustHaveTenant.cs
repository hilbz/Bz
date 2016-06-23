namespace Bz.Domain.Entities
{
    /// <summary>
    /// 实现该接口的实体必须有TenantId
    /// </summary>
    public interface IMustHaveTenant
    {
        /// <summary>
        /// 该实体的TenantId
        /// </summary>
        int TenantId { get; set; }
    }
}
