using Bz.TestBase;
using Castle.MicroKernel.Registration;
using Effort;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using Bz.Collections;
using Bz.Modules;
using SimpleDemo.EntityFramework;
using EntityFramework.DynamicFilters;
using SimpleDemo.Tests.TestDatas;
using SimpleDemo.EntityFramework.Migrations.Seed;

namespace SimpleDemo.Tests
{
    /// <summary>
    /// 所有单元测试类的基类
    /// 初始化BzSystem，模块和模拟类，内存数据库(In-Memory Database)
    /// 初始化种子数据
    /// </summary>
    public class AppTestBase:BzIntegratedTestBase
    {
        protected AppTestBase()
        {
            UsingDbContext(context=> {
                new InitialDbBuilder(context).Create();
                new TestDataBuilder(context).Create();
            });
        }

        protected override void PreInitialize()
        {
            base.PreInitialize();

            //Fake DbConnection using Effort
            LocalIocManager.IocContainer.Register(
                Component.For<DbConnection>()
                    .UsingFactoryMethod(DbConnectionFactory.CreateTransient)
                    .LifestyleSingleton()
                );
        }

        protected override void AddModules(ITypeList<BzModule> modules)
        {
            base.AddModules(modules);

            modules.Add<SimpleDemoTestModule>();
        }

        #region UsingDbContext
        protected void UsingDbContext(Action<SimpleDemoDbContext> action)
        {
            using (var context=LocalIocManager.Resolve<SimpleDemoDbContext>())
            {
                context.DisableAllFilters();
                action(context);
                context.SaveChanges();
            }
        }

        protected async Task UsingDbContextAsync(Action<SimpleDemoDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<SimpleDemoDbContext>())
            {
                context.DisableAllFilters();
                action(context);
                await context.SaveChangesAsync();
            }
        }

        protected T UsingDbContext<T>(Func<SimpleDemoDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<SimpleDemoDbContext>())
            {
                context.DisableAllFilters();
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected async Task<T> UsingDbContextAsync<T>(Func<SimpleDemoDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<SimpleDemoDbContext>())
            {
                context.DisableAllFilters();
                result = await func(context);
                await context.SaveChangesAsync();
            }

            return result;
        }

        #endregion
    }
}
