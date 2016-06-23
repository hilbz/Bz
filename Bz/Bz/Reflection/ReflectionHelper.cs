using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bz.Reflection
{
    /// <summary>
    /// 反射一些帮助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 检查是否<paramref name="giveType"/> implements/inherits <paramref name="genericType"/>
        /// </summary>
        /// <param name="givenType">Type to Check</param>
        /// <param name="genericType">Generic type</param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            if (givenType.IsGenericType&&givenType.GetGenericTypeDefinition()== genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.IsGenericType&& interfaceType.GetGenericTypeDefinition()== genericType)
                {
                    return true;
                }
            }
            if (givenType.BaseType==null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenType.BaseType, genericType);
        }

        /// <summary>
        /// 获取成员和成员的类的特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static List<TAttribute> GetAttributesOfMemberAndDeclaringType<TAttribute>(MemberInfo memberInfo)
            where TAttribute:Attribute
        {
            var attributeList = new List<TAttribute>();

            //添加成员的特性
            if (memberInfo.IsDefined(typeof(TAttribute),true))
            {
                attributeList.AddRange(memberInfo.GetCustomAttributes(typeof(TAttribute),true).Cast<TAttribute>());
            }

            //获取成员类的声明特性
            if (memberInfo.DeclaringType!=null&&memberInfo.DeclaringType.IsDefined(typeof(TAttribute),true))
            {
                attributeList.AddRange(memberInfo.DeclaringType.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());

            }

            return attributeList;
        }

        /// <summary>
        /// 获取成员以及类的单个特性
        /// 如果没有，则返回空
        /// </summary>
        /// <typeparam name="TAttribute">Type Of attribute</typeparam>
        /// <param name="memberInfo">成员</param>
        /// <returns></returns>
        public static TAttribute GetSingleAttributeOfMemberOrDeclaringTypeOrNull<TAttribute>(MemberInfo memberInfo)
            where TAttribute:Attribute
        {
            //获取成员的特性
            if (memberInfo.IsDefined(typeof(TAttribute),true))
            {
                return memberInfo.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>().First();
            }

            //获取一个类的特性
            if (memberInfo.DeclaringType!=null&&memberInfo.DeclaringType.IsDefined(typeof(TAttribute),true))
            {
                return memberInfo.DeclaringType.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>().First();
            }

            return null;
        }
    }
}
