using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Configuration.Startup
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the Bz configuration object.
        /// </summary>
        IBzStartupConfiguration BzConfiguration { get; }
    }
}
