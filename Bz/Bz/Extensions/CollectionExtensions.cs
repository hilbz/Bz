

namespace System.Collections.Generic
{
    /// <summary>
    /// 列表的扩展方法
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 检查给定的Collection是否为空或则没有Item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// 将一个Item添加到列表，如果不存在的话
        /// </summary>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <param name="source">Collection</param>
        /// <param name="item"></param>
        /// <returns>如果有添加返回True,如果已经存在返回false</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source==null)
            {
                throw new ArgumentException("Source为空");
            }
            if (source.Contains(item))
            {
                return false;
            }
            source.Add(item);
            return true;
        }
    }
}
