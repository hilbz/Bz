using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Auditing
{
    /// <summary>
    /// 用于标示一个方法是否需要审计
    /// 如果设计在类上面，就是所有的方法都需要被审计
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class AuditedAttribute:Attribute
    {
    }
}
