using Castle.Core;

namespace Bz
{
    /// <summary>
    /// 集成此接口的类会自动执行初始化在创建的时候
    /// </summary>
    public interface IShouldInitialize : IInitializable
    {

    }
}
