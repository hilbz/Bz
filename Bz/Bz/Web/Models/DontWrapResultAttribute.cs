using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Web.Models
{
    /// <summary>
    /// 禁用包装的简述
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class DontWrapResultAttribute : WrapResultAttribute
    {
        /// <summary>
        /// Gets default <see cref="DontWrapResultAttribute"/>.
        /// </summary>
        public new static DontWrapResultAttribute Default { get { return _default; } }
        private static readonly DontWrapResultAttribute _default = new DontWrapResultAttribute();

        /// <summary>
        /// Initializes a new instance of the <see cref="DontWrapResultAttribute"/> class.
        /// </summary>
        public DontWrapResultAttribute()
            : base(false, false)
        {

        }
    }
}
