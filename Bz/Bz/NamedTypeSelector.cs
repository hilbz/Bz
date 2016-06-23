using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz
{
    /// <summary>
    /// 用于被Named标示的Type选择器
    /// </summary>
    public class NamedTypeSelector
    {
        /// <summary>
        /// 选择器的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表达式树
        /// </summary>
        public Func<Type,bool> Predicate { get; set; }


        public NamedTypeSelector(string name, Func<Type, bool> predicate)
        {
            Name = name;
            Predicate = predicate;
        }
    }
}
