namespace Bz.Web.Configuration
{
    /// <summary>
    /// 用于Configuration Bz Web Module
    /// </summary>
    public interface IBzWebModuleConfiguration
    {
        /// <summary>
        /// 如果设置成True,所有的错误将会直接暴露到前台
        /// 默认值：False
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }
    }
}
