using System;

namespace Bz.Dependency
{
    /// <summary>
    /// 该接口用来包裹一个从IOC Container实例化Resolved的类
    /// 它继承于<see cref="IDisposable"/>,所以被实例化后的Object很容易被释放
    /// 这是个非泛型的版本<see cref="IDisposableDependencyObjectWrapperOfT{T}"/>
    /// </summary>
    public interface IDisposableDependencyObjectWrapper : IDisposableDependencyObjectWrapper<object>
    {

    }
}
