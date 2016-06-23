using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Entities
{
    /// <summary>
    /// Entity扩展方法
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// 检查实体是否被标示成删除，或则为空
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsNullOrDeleted(this ISoftDelete entity)
        {
            return entity == null || entity.IsDeleted;
        }
    }
}
