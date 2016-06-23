using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Entities
{
    [Serializable]
    public class EntityIdentifier
    {
        /// <summary>
        /// Entity Type
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Entity's Id
        /// </summary>
        public object Id { get; set; }

        private EntityIdentifier()
        {

        }

        public EntityIdentifier(Type type, object id)
        {
            if (type==null)
            {
                throw new ArgumentException("Type");
            }
            if (id==null)
            {
                throw new ArgumentException("Id");
            }

            Type = type;
            Id = id;
        }
    }
}
