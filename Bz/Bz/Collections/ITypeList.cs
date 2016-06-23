using System;
using System.Collections.Generic;

namespace Bz.Collections
{
    /// <summary>
    /// object类型的缩写
    /// </summary>
    public interface ITypeList : ITypeList<object>
    {

    }


    /// <summary>
    /// 类型的一些扩展方法
    /// </summary>
    /// <typeparam name="TBaseType"></typeparam>
    public interface ITypeList<in TBaseType> : IList<Type>
    {
        /// <summary>
        /// 把一个类型添加到列表内
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Add<T>() where T : TBaseType;

        /// <summary>
        /// 检查一个类型是不是存在于一个列表中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        bool Contains<T>() where T : TBaseType;

        /// <summary>
        /// 移除一个类型从列表中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Remove<T>() where T : TBaseType;
    }
}
