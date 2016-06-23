using Bz.Dependency;
using Bz.Domain.Entities;
using Bz.EntityFramework.Extensions;
using System;
using System.Reflection;

namespace Bz.EntityFramework.Repositories
{
    internal static class EntityFrameworkGenericRepositoryRegistrar
    {
        public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
        {
            var autoRepositoryAttr = dbContextType.GetSingleAttributeOrNull<AutoRepositoryTypesAttribute>();
            if (autoRepositoryAttr == null)
            {
                autoRepositoryAttr = AutoRepositoryTypesAttribute.Default;
            }

            foreach (var entityType in dbContextType.GetEntityTypes())
            {
                var primaryKeyType = EntityHelper.GetPrimaryKeyType(entityType);
                if (primaryKeyType == typeof(int))
                {
                    var genericRepositoryType = autoRepositoryAttr.RepositoryInterface.MakeGenericType(entityType);
                    if (!iocManager.IsRegistered(genericRepositoryType))
                    {
                        var implType = autoRepositoryAttr.RepositoryImplementation.GetGenericArguments().Length == 1
                                ? autoRepositoryAttr.RepositoryImplementation.MakeGenericType(entityType)
                                : autoRepositoryAttr.RepositoryImplementation.MakeGenericType(dbContextType, entityType);

                        iocManager.Register(
                            genericRepositoryType,
                            implType,
                            DependencyLifeStyle.Transient
                            );
                    }
                }
                //注入两次的原因:IRespository<Bz>,IRespository<Bz,int>都可以用
                var genericRepositoryTypeWithPrimaryKey = autoRepositoryAttr.RepositoryInterfaceWithPrimaryKey.MakeGenericType(entityType, primaryKeyType);
                if (!iocManager.IsRegistered(genericRepositoryTypeWithPrimaryKey))
                {
                    var implType = autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.GetGenericArguments().Length == 2
                                ? autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.MakeGenericType(entityType, primaryKeyType)
                                : autoRepositoryAttr.RepositoryImplementationWithPrimaryKey.MakeGenericType(dbContextType, entityType, primaryKeyType);

                    iocManager.Register(
                        genericRepositoryTypeWithPrimaryKey,
                        implType,
                        DependencyLifeStyle.Transient
                        );
                }
            }
        }

    }
}
