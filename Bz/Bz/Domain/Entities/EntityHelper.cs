using System;

namespace Bz.Domain.Entities
{
    /// <summary>
    /// Entity Helper
    /// </summary>
    public static class EntityHelper
    {

        public static Type GetPrimaryKeyType<TEntity>()
        {
            return GetPrimaryKeyType(typeof(TEntity));
        }

        /// <summary>
        /// 从实体上获取主键类型
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetInterfaces())
            {
                if (interfaceType.IsGenericType&&interfaceType.GetGenericTypeDefinition()==typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }

            }
            throw new BzException("未能找到主键类型，确定是否有继承的IEntity<>");

        }
    }
}
