using Bz.Dapper.Configuration;
using System.Configuration;
using System.Data.SqlClient;

namespace Bz.DapperWrapper
{
    /// <summary>
    /// DapperWrapper的扩展方法
    /// </summary>
    public static class DapperWrapperExtensions
    {
        /// <summary>
        /// 获取特定连接的<see cref="IDbExecutor"/>
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDbExecutor CreateExecutor(this IDbExecutorFactory factory, DbConfigType type)
        {
            var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[DbConfiguration.GetConnectionName(type)].ConnectionString);
            return new SqlExecutor(dbConnection);
        }
    }
}
