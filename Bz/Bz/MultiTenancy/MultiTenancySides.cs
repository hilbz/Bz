using System;

namespace Bz.MultiTenancy
{
    /// <summary>
    /// 用于一个多租户的应用
    /// 标示成Flags可以进行位运算
    /// </summary>
    [Flags]
    public enum MultiTenancySides
    {
        /// <summary>
        /// 站点端
        /// </summary>
        Tenant = 1,

        /// <summary>
        /// 超级管理员side
        /// </summary>
        Host = 2
    }
}
