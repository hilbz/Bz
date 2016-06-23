using Bz.Dependency;
using Bz.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Application.Services
{
    /// <summary>
    /// 此接口应该被应用层的所有类集成
    /// </summary>    
    public abstract class ApplicationService : BzServiceBase, IApplicationService, ITransientDependency
    {
        /// <summary>
        /// 获取当前session information.
        /// </summary>
        public IBzSession BzSession { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        protected ApplicationService()
        {
            BzSession = NullBzSession.Instance;
        }
    }
}
