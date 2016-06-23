using Bz.Collections;
using Bz.Modules;
using System;
using System.Collections.Generic;

namespace Bz.TestBase.Modules
{
    public class TestModuleFinder : IModuleFinder
    {
        public ITypeList<BzModule> Modules { get; private set; }

        public TestModuleFinder()
        {
            Modules = new TypeList<BzModule>();
        }

        public ICollection<Type> FindAll()
        {
            return Modules;
        }
    }
}
