using System;

namespace Bz.Dapper.Configuration
{
    /// <summary>
    ///     数据库连接配置
    /// </summary>
    public static class DbConfiguration
    {
        /// <summary>
        ///     获取连接字符串名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetConnectionName(DbConfigType type)
        {
            switch (type)
            {
                case DbConfigType.Default:
                    return "Default";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    /// <summary>
    ///     数据库配置
    /// </summary>
    public enum DbConfigType
    {
        /// <summary>
        ///     默认主库
        /// </summary>
        Default
    }
}
