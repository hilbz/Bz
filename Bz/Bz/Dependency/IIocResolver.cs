using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    /// <summary>
    /// 定义一些接口来实话已经注册的类
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 获取一个对象从Ioc Container.
        /// 对象再用完必须被释放掉
        /// </summary>
        /// <typeparam name="T">Type of the object to get（IBzService）</typeparam>
        /// <returns>对象实例</returns>
        T Resolve<T>();

        /// <summary>
        /// 获取一个对象从Ioc Container，一个接口会有多个实例对象.
        /// 对象再用完必须被释放掉
        /// </summary>
        /// <typeparam name="T">Type of the object to get(IController)</typeparam>
        /// <param name="type">BzController</param>
        /// <returns>对象实例</returns>
        T Resolve<T>(Type type);

        /// <summary>
        /// 获取一个对象从Ioc Container.
        /// 对象再用完必须被释放掉
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="argumentsAsAnonymousType">构造参数</param>
        /// <returns>对象实例</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// 从IOC Container获取一个对象.
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <returns>对象实例</returns>
        object Resolve(Type type);

        /// <summary>
        /// 从IOC Container获取一个对象.
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <param name="argumentsAsAnonymousType">构造参数</param>
        /// <returns>对象实例</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// 释放一个已经被实例的对象
        /// </summary>
        /// <param name="obj">将被释放的对象</param>
        void Release(object obj);

        /// <summary>
        /// 判断给定的Type是否已经被注入
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns></returns>
        bool IsRegistered(Type type);

        /// <summary>
        /// 判断给定的Type是否已经被注入
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        /// <returns></returns>
        bool IsRegistered<T>();
    }
}
