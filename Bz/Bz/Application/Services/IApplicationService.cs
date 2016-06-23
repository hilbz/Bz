using Bz.Dependency;

namespace Bz.Application.Services
{
    /// <summary>
    /// 此接口应该被应用层的所有类集成
    /// </summary>    
    public interface IApplicationService:ITransientDependency
    {
    }
}
