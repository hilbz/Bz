using System;
using System.Data.SqlClient;

namespace Bz.DapperWrapper
{
    public class SqlExecutorFactory : IDbExecutorFactory
    {
        readonly string _connectionString;

        public SqlExecutorFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");
            _connectionString = connectionString;
        }

        public IDbExecutor CreateExecutor()
        {
            var dbConnection = new SqlConnection(_connectionString);
            return new SqlExecutor(dbConnection);
        }
    }
}
