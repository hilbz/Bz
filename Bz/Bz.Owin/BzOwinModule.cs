using Bz.Modules;

namespace Bz.Owin
{

    /// <summary>
    /// OWIN 实现模块 Bz
    /// </summary>
    [DependsOn(typeof(BzKernelModule))]
    public class BzOwinModule:BzModule
    {
    }
}
