using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    public class ConventionalRegistrationContext:IConventionalRegistrationContext
    {
        /// <summary>
        /// 注册的程序集
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// IOC Container的管理器
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// 注册配置
        /// </summary>
        public ConventionalRegistrationConfig Config { get; private set; }

        internal ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager, ConventionalRegistrationConfig config)
        {
            Assembly = assembly;
            IocManager = iocManager;
            Config = config;
        }
    }
}
