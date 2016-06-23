using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Modules
{
    internal interface IBzModuleManager
    {
        void InitializeModules();

        void ShutdownModules();
    }
}
