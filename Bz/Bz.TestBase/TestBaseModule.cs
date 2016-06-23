using Bz.Modules;
using System.Reflection;

namespace Bz.TestBase
{
    public class TestBaseModule:BzModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
