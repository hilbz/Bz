using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    /// <summary>
    /// 这个接口来用直接管理IOC依赖注入
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// 引用The Castle Windsor Container.
        /// </summary>
        IWindsorContainer IocContainer { get; }
        /// <summary>
        /// 判断给定的Type是否已经被注入
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns></returns>
        new bool IsRegistered(Type type);

        /// <summary>
        /// 判断给定的Type是否已经被注入
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        /// <returns></returns>
        new bool IsRegistered<T>();
    }
}
