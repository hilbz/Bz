namespace Bz.Domain.Entities
{
    /// <summary>
    /// 是否激活
    /// </summary>
    public interface IPassivable
    {
        /// <summary>
        /// True:该实体是激活的
        /// False:该实体不是激活的
        /// </summary>
        bool IsActive { get; set; }
    }
}
