using Bz.Dapper.Configuration;
using Bz.DapperWrapper;
using Bz.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDemo.Dapper.Query
{
    public class TestQuery: ITestQuery, ITransientDependency
    {
        private readonly IDbExecutorFactory _dbExecutorFactory;
        public TestQuery(IDbExecutorFactory dbExecutorFactory)
        {
            _dbExecutorFactory = dbExecutorFactory;
        }

        /// <summary>
        /// 测试Dapper的读取
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetStringAsync(int keyWord)
        {
            var dbExecutor = _dbExecutorFactory.CreateExecutor(DbConfigType.Default);
            var identityUser =
                await
                    dbExecutor.QueryAsync<string>(
                        "SELECT Name FROM lbz WHERE Id=@keyWord", new { keyWord });

            var result = identityUser.SingleOrDefault();
            return result;
        }
    }
}
