using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    /// <summary>
    /// 扩展方法 <see cref="IIocResolver"/>接口
    /// </summary>
    public static class IocResolverExtensions
    {
        #region ResolveAsDisposable

        /// <summary>
        /// 包装一个已经resolved的实体，保证用完IT其释放
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="iocResolver">IIocResolver object</param>
        /// <returns>返回包裹的对象</returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, iocResolver.Resolve<T>());
        }

        /// <summary>
        /// 包装一个已经resolved的实体，保证用完IT其释放
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="iocResolver">IIocResolver object</param>
        /// <param name="type">Type of the object to resolve. This type must be 可转化 <see cref="T"/></param>
        /// <returns>返回包裹的对象</returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, Type type)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, (T)iocResolver.Resolve(type));
        }

        /// <summary>
        /// 包装一个已经resolved的实体，保证用完IT其释放
        /// </summary>
        /// <param name="iocResolver">IIocResolver object</param>
        /// <param name="type">可以释放的类型<see cref="IDisposable"/></param>
        /// <returns>对象的包装类</returns>
        public static IDisposableDependencyObjectWrapper ResolveAsDisposable(this IIocResolver iocResolver, Type type)
        {
            return new DisposableDependencyObjectWrapper(iocResolver, iocResolver.Resolve(type));
        }

        /// <summary>
        /// 包装一个已经resolved的实体，保证用完IT其释放
        /// </summary>
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="iocResolver">IIocResolver object</param>
        /// <param name="argumentsAsAnonymousType">构造函数</param>
        /// <returns>对象的包装类</returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, object argumentsAsAnonymousType)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, iocResolver.Resolve<T>(argumentsAsAnonymousType));
        }

        /// <summary>
        /// 包装一个已经resolved的实体，保证用完IT其释放 <see cref="DisposableDependencyObjectWrapper{T}"/> be Disposable.
        /// </summary> 
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <param name="iocResolver">IIocResolver object</param>
        /// <param name="type">Type of the object to resolve. This type must be 可转化<see cref="T"/>.</param>
        /// <param name="argumentsAsAnonymousType">构造函数</param>
        /// <returns>对象实例wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/></returns>
        public static IDisposableDependencyObjectWrapper<T> ResolveAsDisposable<T>(this IIocResolver iocResolver, Type type, object argumentsAsAnonymousType)
        {
            return new DisposableDependencyObjectWrapper<T>(iocResolver, (T)iocResolver.Resolve(type, argumentsAsAnonymousType));
        }


        /// <summary>
        /// 包装一个已经resolved的实体，保证用完IT其释放 <see cref="DisposableDependencyObjectWrapper{T}"/> be Disposable.
        /// </summary> 
        /// <param name="iocResolver">IIocResolver object</param>
        /// <param name="type">Type of the object to resolve. This type must be convertible to <see cref="IDisposable"/>.</param>
        /// <param name="argumentsAsAnonymousType">构造函数</param>
        /// <returns>对象实例 wrapped by <see cref="DisposableDependencyObjectWrapper{T}"/></returns>
        public static IDisposableDependencyObjectWrapper ResolveAsDisposable(this IIocResolver iocResolver, Type type, object argumentsAsAnonymousType)
        {
            return new DisposableDependencyObjectWrapper(iocResolver, iocResolver.Resolve(type, argumentsAsAnonymousType));
        }
        #endregion

        #region Using
        /// <summary>
        /// 这个方法用来自动实例化和释放对象
        /// </summary>
        /// <typeparam name="T">Type of the object to use</typeparam>
        /// <param name="iocResolver">IIocResolver object</param>
        /// <param name="action">An 委托可以用已经resolved好的Object</param>
        public static void Using<T>(this IIocResolver iocResolver, Action<T> action)
        {
            using (var wrapper=new DisposableDependencyObjectWrapper<T>(iocResolver, iocResolver.Resolve<T>()))
            {
                action(wrapper.Object);
            }
        }
        #endregion
    }
}
