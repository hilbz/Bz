using Bz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Repositories
{
    /// <summary>
    /// 一个简写<see cref="IRepository{TEntity, TPrimaryKey}"/>,用<see cref="int"/>主键
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity>:IRepository<TEntity,int> where TEntity:class,IEntity<int>
    {
    }
}
