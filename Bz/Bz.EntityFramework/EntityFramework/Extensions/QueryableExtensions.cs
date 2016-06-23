using Bz.Domain.Entities;
using Bz.Reflection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Bz.EntityFramework.Extensions
{
   /// <summary>
   /// 基于<see cref="IQueryable"/>的扩展
   /// </summary>
    internal static class QueryableExtensions
    {
        /// <summary>
        /// 关系对象的包含扩展
        /// </summary>
        /// <param name="source">源.</param>
        /// <param name="condition">条件 <see cref="path"/> or not.</param>
        /// <param name="path">需要加载的分隔.</param>
        public static IQueryable IncludeIf(this IQueryable source, bool condition, string path)
        {
            return condition
                ? source.Include(path)
                : source;
        }

        /// <summary>
        /// 关系对象的包含扩展
        /// </summary>
        /// <param name="source">源.</param>
        /// <param name="condition">条件 <see cref="path"/> or not.</param>
        /// <param name="path">需要加载的分隔.</param>

        public static IQueryable<T> IncludeIf<T>(this IQueryable<T> source, bool condition, string path)
        {
            return condition
                ? source.Include(path)
                : source;
        }

        /// <summary>
        /// 关系对象的包含扩展
        /// </summary>
        /// <param name="source">源.</param>
        /// <param name="condition">条件 <see cref="path"/> or not.</param>
        /// <param name="path">需要加载的分隔.</param>

        public static IQueryable<T> IncludeIf<T, TProperty>(this IQueryable<T> source, bool condition, Expression<Func<T, TProperty>> path)
        {
            return condition
                ? source.Include(path)
                : source;
        }
    }
}
