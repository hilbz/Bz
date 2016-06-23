using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Configuration.Startup
{
    /// <summary>
    /// 配置多租户
    /// </summary>
    public interface IMultiTenancyConfig
    {
        /// <summary>
        /// 是否启用多站点?
        /// Default value: false.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
