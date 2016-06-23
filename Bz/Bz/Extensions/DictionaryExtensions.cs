using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    /// <summary>
    /// Dictionary的扩展方法
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 用于获取一个字典的值如果存在
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="dictionary">Oject的字典</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value Of Key(or default value if key not exists)</param>
        /// <returns>True 若查得到</returns>
        public static bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
        {
            object valueObj;
            if (dictionary.TryGetValue(key,out valueObj)&& valueObj is T)
            {
                value = (T)valueObj;
                return true;
            }
            value = default(T);
            return false;
        }

        /// <summary>
        /// 根据指定的Key获取字典的Value,如果没有存在则返回default(TValue)
        /// </summary>
        /// <typeparam name="TKey">Type Of the key</typeparam>
        /// <typeparam name="TValue">Type Of the value</typeparam>
        /// <param name="dictionary">Dictionary to check and get</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue obj;
            return dictionary.TryGetValue(key, out obj) ? obj : default(TValue);
        }
    }
}
