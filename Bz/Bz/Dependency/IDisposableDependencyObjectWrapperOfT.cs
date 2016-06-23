using System;

namespace Bz.Dependency
{
    /// <summary>
    /// 该接口用来包裹一个从IOC Container实例化Resolved的类
    /// 它继承于<see cref="IDisposable"/>,所以被实例化后的Object很容易被释放
    /// </summary>
    /// <typeparam name="T">Type of the object</typeparam>
    public interface IDisposableDependencyObjectWrapper<out T>:IDisposable
    {
        /// <summary>
        /// 需要被实例化的实体
        /// </summary>
        T Object { get; }
    }
}
