using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Runtime.Validation
{
    /// <summary>
    /// 只能添加到方法上，用于禁用验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DisableValidationAttribute:Attribute
    {
    }
}
