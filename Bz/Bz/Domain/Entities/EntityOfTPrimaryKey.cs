using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Domain.Entities
{
    /// <summary>
    /// 基本的实现IEntity 接口
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型Type</typeparam>
    [Serializable]
    public abstract class Entity<TPrimaryKey>:IEntity<TPrimaryKey>
    {

        public virtual TPrimaryKey Id { get; set; }

        /// <summary>
        /// 判断当前实体是不是已经持久化到数据库了
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTransient()
        {
            return EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey));
        }

        public override bool Equals(object obj)
        {
            if (obj==null||!(obj is Entity<TPrimaryKey>))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            var other=(Entity<TPrimaryKey>)obj;
            if (IsTransient()&&other.IsTransient())
            {
                return false;
            }

            //对比的类型必须是一致
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();

            if (!typeOfThis.IsAssignableFrom(typeOfOther)&&!typeOfOther.IsAssignableFrom(typeOfThis))
            {
                return false;
            }
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }
            return left.Equals(right);
        }

        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left==right);
        }

        public override string ToString()
        {
            return string.Format("[{0}{1}]",GetType().Name,Id);
        }
    }
}
