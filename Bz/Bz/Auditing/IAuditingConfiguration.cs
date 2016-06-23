using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Auditing
{
    /// <summary>
    /// 用于配置审计日记
    /// </summary>
    public interface IAuditingConfiguration
    {
        /// <summary>
        /// 用于设置系统的审计日记可不可用
        /// 默认：True。如果设置False将完全禁用它
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// 如果设置成True，未登录用户也能进行审计
        /// Default:False
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// 用于配置MVC Controller的审计日记
        /// </summary>
        IMvcControllersAuditingConfiguration MvcControllers { get; set; }

        /// <summary>
        /// 用于筛选默认需要被设计的类/接口
        /// </summary>
        IAuditingSelectorList Selectors { get; }
    }
}
