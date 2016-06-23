using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Auditing
{
    /// <summary>
    /// 定义MVC Controller的审计配置
    /// </summary>
    public interface IMvcControllersAuditingConfiguration
    {
        /// <summary>
        /// 用于对MVC Controller进行审计 
        /// 默认：true
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// 用于启用或则禁用 审计For child Mvc Actions
        /// 默认:false.不对child mvc action进行审计
        /// </summary>
        bool IsEnabledForChildActions { get; set; }
    }
}
