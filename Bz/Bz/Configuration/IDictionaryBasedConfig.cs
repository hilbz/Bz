using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Configuration
{

    /// <summary>
    /// 使用一个字典配置的定义
    /// </summary>
    public interface IDictionaryBasedConfig
    {
        /// <summary>
        /// Used To Set a string named configuration.
        /// 如果存在这个名称<paramref name="name"/>,将会被覆盖.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void Set<T>(string name, T value);

        /// <summary>
        /// 根据名称获取配置
        /// </summary>
        /// <param name="name">唯一的配置名称</param>
        /// <returns>配置的Value,没找到返回null</returns>
        object Get(string name);

        /// <summary>
        /// 根据名称获取配置
        /// </summary>
        /// <typeparam name="T">Type Of the object</typeparam>
        /// <param name="name">唯一的名称</param>
        /// <returns>配置的Value,没找到返回null</returns>
        T Get<T>(string name);

        /// <summary>
        /// 根据名称获取配置
        /// </summary>
        /// <param name="name">唯一的名称</param>
        /// <param name="defaultValue">如果没取到就用默认值</param>
        /// <returns>配置的值，没找到取defaultValue</returns>
        object Get(string name, object defaultValue);

        /// <summary>
        /// 根据名称获取配置
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="name">唯一的名称</param>
        /// <param name="defaultValue">如果没取到就用默认值</param>
        /// <returns>配置的值，没找到取defaultValue</returns>
        T Get<T>(string name, T defaultValue);

        /// <summary>
        /// 根据名称获取配置，为空则创建
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="name"></param>
        /// <param name="creator">当取值为空的时候会调用此函数来创建</param>
        /// <returns>配置的值，没找到取null</returns>
        T GetOrCreate<T>(string name, Func<T> creator);
    }
}
