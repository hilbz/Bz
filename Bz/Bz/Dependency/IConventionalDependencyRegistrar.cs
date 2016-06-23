using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    /// <summary>
    /// 用来定义一些在加载程序时约定的注入
    /// </summary>
    public interface IConventionalDependencyRegistrar
    {

        /// <summary>
        /// 根据指定程序集注册Type
        /// </summary>
        /// <param name="context">注册包装上下文</param>
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}
