using Bz.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.EntityFramework.Repositories
{
    /// <summary>
    /// 自动注入仓储实体到DbContext里面
    /// 特别是多个DbContext的时候很有用
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute:Attribute
    {
        public static AutoRepositoryTypesAttribute Default { get; private set; }

        public Type RepositoryInterface { get; private set; }

        public Type RepositoryInterfaceWithPrimaryKey { get; private set; }

        public Type RepositoryImplementation { get; private set; }

        public Type RepositoryImplementationWithPrimaryKey { get; private set; }

        static AutoRepositoryTypesAttribute()
        {
            Default = new AutoRepositoryTypesAttribute(
                typeof(IRepository<>),
                typeof(IRepository<,>),
                typeof(EfRepositoryBase<,>),
                typeof(EfRepositoryBase<,,>)
                );
        }

        public AutoRepositoryTypesAttribute(
            Type repositoryInterface,
            Type repositoryInterfaceWithPrimaryKey,
            Type repositoryImplementation,
            Type repositoryImplementationWithPrimaryKey)
        {
            RepositoryInterface = repositoryInterface;
            RepositoryInterfaceWithPrimaryKey = repositoryInterfaceWithPrimaryKey;
            RepositoryImplementation = repositoryImplementation;
            RepositoryImplementationWithPrimaryKey = repositoryImplementationWithPrimaryKey;
        }
    }
}
