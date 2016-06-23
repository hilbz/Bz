using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    /// <summary>
    /// 包装了一个上下文用于约定注入
    /// </summary>
    public interface IConventionalRegistrationContext
    {
        /// <summary>
        /// 需要注册的程序集
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// IocManger的引用，对IOC Container的包装
        /// </summary>
        IIocManager IocManager { get;}

        /// <summary>
        /// 约定注册的配置
        /// </summary>
        ConventionalRegistrationConfig Config { get; }


    }
}
