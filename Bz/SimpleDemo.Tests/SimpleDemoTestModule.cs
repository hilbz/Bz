using Bz.Modules;
using SimpleDemo.Application;
using SimpleDemo.EntityFramework;

namespace SimpleDemo.Tests
{
    [DependsOn(
        typeof(SimpleDemoApplicationModule),
        typeof(SimpleDemoDataModule)
        )]
    public class SimpleDemoTestModule:BzModule
    {
    }
}
