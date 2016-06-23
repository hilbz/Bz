using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Entities
{
    /// <summary>
    /// 一个简述<see cref="IEntity{TPrimaryKey}" for most Used primary key type<see cref="int"/>/>
    /// </summary>
    public interface IEntity:IEntity<int>
    {
    }
}
