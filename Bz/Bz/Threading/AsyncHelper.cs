using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Threading
{
    /// <summary>
    /// 提供一些助手对一些异步操作进行处理
    /// </summary>
    public static class AsyncHelper
    {
        /// <summary>
        /// 检查给定的方法是否是异步方法
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task)||
                (method.ReturnType.IsGenericType&&method.ReturnType.GetGenericTypeDefinition()==typeof(Task<>))
                );
        }

        /// <summary>
        /// 以同步的方式运行一个异步方法
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return AsyncContext.Run(func);
        }

        /// <summary>
        /// 以同步的方式运行一个异步方法.
        /// </summary>
        /// <param name="action">An async action</param>
        public static void RunSync(Func<Task> action)
        {
            AsyncContext.Run(action);
        }
    }
}
