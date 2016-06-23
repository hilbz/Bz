using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Configuration.Startup
{
    /// <summary>
    /// 用于配置多站点
    /// </summary>
    internal class MultiTenancyConfig:IMultiTenancyConfig
    {
        /// <summary>
        /// 多站点是否启用
        /// 默认值：不启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
