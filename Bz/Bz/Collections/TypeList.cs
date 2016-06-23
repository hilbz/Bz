using System;
using System.Collections;
using System.Collections.Generic;

namespace Bz.Collections
{
    public class TypeList<TBaseType>:ITypeList<TBaseType>
    {
        /// <summary>
        /// 列表的数量
        /// </summary>
        public int Count { get { return _typeList.Count; } }

        /// <summary>
        /// 用于指示该实例是否是只读的
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// 设置索引器x.
        /// </summary>
        /// <param name="index">Index.</param>
        public Type this[int index]
        {
            get { return _typeList[index]; }
            set
            {
                CheckType(value);
                _typeList[index] = value;
            }
        }

        private readonly List<Type> _typeList;

        public TypeList()
        {
            _typeList = new List<Type>();
        }

        /// <inheritdoc/>
        public void Add<T>() where T : TBaseType
        {
            _typeList.Add(typeof(T));
        }

        /// <inheritdoc/>
        public void Add(Type item)
        {
            CheckType(item);
            _typeList.Add(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, Type item)
        {
            _typeList.Insert(index, item);
        }

        /// <inheritdoc/>
        public int IndexOf(Type item)
        {
            return _typeList.IndexOf(item);
        }

        /// <inheritdoc/>
        public bool Contains<T>() where T : TBaseType
        {
            return Contains(typeof(T));
        }


        /// <inheritdoc/>
        public bool Contains(Type item)
        {
            return _typeList.Contains(item);
        }

        /// <inheritdoc/>
        public void Remove<T>() where T : TBaseType
        {
            _typeList.Remove(typeof(T));
        }

        /// <inheritdoc/>
        public bool Remove(Type item)
        {
            return _typeList.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            _typeList.RemoveAt(index);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _typeList.Clear();
        }

        /// <inheritdoc/>
        public void CopyTo(Type[] array, int arrayIndex)
        {
            _typeList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).IsAssignableFrom(item))
            {
                throw new ArgumentException("给定的类型不是有效的类型："+typeof(TBaseType));
            }
        }
    }
}
