using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Entities
{
    /// <summary>
    /// 定义对实体类的一个接口.所有类在BzSystem都必须实现此接口
    /// </summary>
    /// <typeparam name="TPrimaryKey">实体的主键</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 实体的主键
        /// </summary>
        TPrimaryKey Id { get; set; }

        /// <summary>
        /// 检查该实体是不是临时的（还没持久化到数据库，没有<see cref="Id"/>值）
        /// </summary>
        /// <returns></returns>
        bool IsTransient();
    }
}
