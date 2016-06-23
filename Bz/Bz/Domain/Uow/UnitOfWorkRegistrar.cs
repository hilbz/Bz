using Bz.Dependency;
using System.Linq;
using System.Reflection;

namespace Bz.Domain.Uow
{
    /// <summary>
    /// 注册拦截器Uow
    /// </summary>
    internal static class UnitOfWorkRegistrar
    {
        /// <summary>
        /// 初始化注册器
        /// </summary>
        /// <param name="iocManager"></param>
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            if (UnitOfWorkHelper.IsConventionalUowClass(handler.ComponentModel.Implementation))
            {
                //拦截所有应用层和仓储层方法
                handler.ComponentModel.Interceptors.Add(new Castle.Core.InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
            else if (handler.ComponentModel.Implementation.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(UnitOfWorkHelper.HasUnitOfWorkAttribute))
            {
                //还拦截UOWattribute的方法
                handler.ComponentModel.Interceptors.Add(new Castle.Core.InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }
    }
}
