using Bz.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    /// <summary>
    /// 用于需要约定注册类的配置
    /// </summary>
    public class ConventionalRegistrationConfig:DictionaryBasedConfig
    {
        /// <summary>
        /// 是否自动安装拦截器<see cref=""/>
        /// Defalut:true.
        /// </summary>
        public bool InstallInstallers { get; set;}

        /// <summary>
        /// 创建一个新的<see cref="ConventionalRegistrationConfig"/>object
        /// </summary>
        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}
