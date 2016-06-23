using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Configuration.Startup
{
    public class ModuleConfigurations:IModuleConfigurations
    {
        public IBzStartupConfiguration BzConfiguration { get; private set; }

        public ModuleConfigurations(IBzStartupConfiguration bzConfiguration)
        {
            BzConfiguration = bzConfiguration;
        }
    }
}
