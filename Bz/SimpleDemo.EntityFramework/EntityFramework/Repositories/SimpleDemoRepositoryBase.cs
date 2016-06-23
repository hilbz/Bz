using Bz.Domain.Entities;
using Bz.EntityFramework;
using Bz.EntityFramework.Repositories;

namespace SimpleDemo.EntityFramework.Repositories
{
    public abstract class SimpleDemoRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<SimpleDemoDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected SimpleDemoRepositoryBase(IDbContextProvider<SimpleDemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
        //添加自己一些通用的方法For all Repositories
    }

    public abstract class SimpleDemoRepositoryBase<TEntity> : EfRepositoryBase<SimpleDemoDbContext, TEntity, int>
    where TEntity : class, IEntity<int>
    {
        protected SimpleDemoRepositoryBase(IDbContextProvider<SimpleDemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
        //这里不要添加任何方法
    }
}
