using Bz.Dependency;

namespace Bz.Domain.Services
{
    /// <summary>
    /// 领域服务，当一个业务需要跨两个不相关实体查询时候，需要用到
    /// </summary>
    public interface IDomainService : ITransientDependency
    {
    }
}
