using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Entities
{
    /// <summary>
    /// 简写<see cref="Entity{TPrimaryKey}"/>
    /// </summary>
    [Serializable]
    public abstract class Entity:Entity<int>,IEntity
    {
    }
}
