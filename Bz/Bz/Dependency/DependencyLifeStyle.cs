using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Dependency
{
    /// <summary>
    /// 依赖注入系统的生命周期类型
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// 单例注入，即就第一次进行实例化
        /// 在之后取的类都是第一次实例化的实体
        /// </summary>
        Singleton,
        
        /// <summary>
        /// 临时类.每一次resolving都将创建一个新的实例
        /// </summary>
        Transient
    }
}
