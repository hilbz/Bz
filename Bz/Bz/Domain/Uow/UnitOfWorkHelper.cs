using Bz.Application.Services;
using Bz.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// UOW助手类
    /// </summary>
    internal static class UnitOfWorkHelper
    {
        /// <summary>
        /// UOW必须约定于特定的类
        /// </summary>
        /// <param name="type">需要检查的Type</param>
        /// <returns></returns>
        public static bool IsConventionalUowClass(Type type)
        {
            return typeof(IRepository).IsAssignableFrom(type) ||
                typeof(IApplicationService).IsAssignableFrom(type);
        }

        /// <summary>
        /// 给定的方法是否有UOW特性
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static bool HasUnitOfWorkAttribute(MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }

        /// <summary>
        /// 获取成员的UOW特性，不存在返回null.
        /// </summary>
        /// <param name="methodInfo">Method info to check</param>
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof(UnitOfWorkAttribute), false);
            if (attrs.Length <= 0)
            {
                return null;
            }

            return (UnitOfWorkAttribute)attrs[0];
        }
    }
}
