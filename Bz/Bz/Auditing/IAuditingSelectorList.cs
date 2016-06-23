using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Auditing
{
    /// <summary>
    /// 选择列表用于筛选哪些接口/类需要被审计
    /// </summary>
    public interface IAuditingSelectorList:IList<NamedTypeSelector>
    {
        /// <summary>
        /// 根据name删除一个selector
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool RemoveByName(string name);
    }
}
