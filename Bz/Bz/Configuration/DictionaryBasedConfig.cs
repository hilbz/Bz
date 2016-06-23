using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Configuration
{
    /// <summary>
    /// 用于 设置/获取 自定义配置
    /// </summary>
    public class DictionaryBasedConfig:IDictionaryBasedConfig
    {
        /// <summary>
        /// 自定义的字典配置
        /// </summary>
        protected Dictionary<string,object> CustomSettings { get; set; }

        /// <summary>
        /// 获取配置的值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>配置的值</returns>
        public object this[string name]
        {
            get { return CustomSettings.GetOrDefault(name); }
            set { CustomSettings[name] = value; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        protected DictionaryBasedConfig()
        {
            CustomSettings = new Dictionary<string, object>();
        }

        /// <summary>
        /// Used To Set a string named configuration.
        /// 如果存在这个名称<paramref name="name"/>,将会被覆盖.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Set<T>(string name, T value)
        {
            this[name] = value;
        }

        /// <summary>
        /// 根据名称获取配置
        /// </summary>
        /// <param name="name">唯一的配置名称</param>
        /// <returns>配置的Value,没找到返回null</returns>
        public object Get(string name)
        {
            return Get(name, null);
        }

        /// <summary>
        /// 根据名称获取配置
        /// </summary>
        /// <param name="name">唯一的名称</param>
        /// <param name="defaultValue">如果没取到就用默认值</param>
        /// <returns>配置的值，没找到取defaultValue</returns>
        public object Get(string name, object defaultValue)
        {
            var value = this[name];
            return value == null ? defaultValue : value;
        }

        /// <summary>
        /// 根据Key或则配置value，并转化为特定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">Type of the config</param>
        /// <returns>value，若为空则取default(T)</returns>
        public T Get<T>(string name)
        {
            var value = this[name];
            return value == null
                ? default(T)
                : (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// 根据名称获取配置
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="name">唯一的名称</param>
        /// <param name="defaultValue">如果没取到就用默认值</param>
        /// <returns>配置的值，没找到取defaultValue</returns>
        public T Get<T>(string name, T defaultValue)
        {
            return (T)Get(name, (object)defaultValue);
        }

        /// <summary>
        /// 根据名称获取配置，为空则创建
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="name"></param>
        /// <param name="creator">当取值为空的时候会调用此函数来创建</param>
        /// <returns>配置的值，没找到取null</returns>
        public T GetOrCreate<T>(string name, Func<T> creator)
        {
            var value = Get(name);
            if (value==null)
            {
                value = creator();
                Set(name, value);
            }
            return (T)value;
        }
    }
}
