using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    /// <summary>
    /// 扩展方法<see cref="IIocRegistrar"/>Interface
    /// </summary>
    public static class IocRegistrarExtensions
    {
        #region RegisterIfNot
        /// <summary>
        /// 注册一个Type如果这个类型没有被注册过
        /// </summary>
        /// <typeparam name="T">Type of the class</typeparam>
        /// <param name="iocRegistrar">Registrar</param>
        /// <param name="lifeStyle">该Type的生命周期</param>
        /// <returns>True,该类型未注册过，则注册该类并返回True</returns>
        public static bool RegisterIfNot<T>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T :class
        {
            if (iocRegistrar.IsRegistered<T>())
            {
                return false;
            }
            iocRegistrar.Register<T>(lifeStyle);
            return true;
        }
        /// <summary>
        /// 注册一个Type如果这个类型没有被注册过
        /// </summary>
        /// <param name="iocRegistrar">Registrar</param>
        /// <param name="type">ype of the class</param>
        /// <param name="lifeStyle">该Type的生命周期</param>
        /// <returns>True,该类型未注册过，则注册该类并返回True</returns>
        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar,Type type,DependencyLifeStyle lifeStyle=DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }
            iocRegistrar.Register(type,lifeStyle);
            return true;
        }

        /// <summary>
        /// 注册一个Type根据他的实现，如果这个类型没有被注册过
        /// </summary>
        /// <typeparam name="TType">需要注册的类IBzService</typeparam>
        /// <typeparam name="TImpl">实现类BzService</typeparam>
        /// <param name="iocRegistrar">Registrar</param>
        /// <param name="lifeStyle">类型的生命周期：单例或则临时类</param>
        /// <returns></returns>
        public static bool RegisterIfNot<TType,TImpl>(this IIocRegistrar iocRegistrar,DependencyLifeStyle lifeStyle=DependencyLifeStyle.Singleton)
            where TType:class
            where TImpl:class,TType
        {
            if (iocRegistrar.IsRegistered<TType>())
            {
                return false;
            }
            iocRegistrar.Register<TType, TImpl>(lifeStyle);
            return true;
        }

        /// <summary>
        /// 注册一个Type根据他的实现，如果这个类型没有被注册过
        /// </summary>
        /// <param name="iocRegistrar">Registrar</param>
        /// <param name="type">需要注册的类IBzService</param>
        /// <param name="impl">实现类BzService</param>
        /// <param name="lifeStyle">类型的生命周期：单例或则临时类</param>
        /// <returns></returns>
        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }
            iocRegistrar.Register(type, impl, lifeStyle);
            return true;
        }
        #endregion
    }
}
