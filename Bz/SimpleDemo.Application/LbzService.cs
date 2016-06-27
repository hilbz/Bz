using Bz.Domain.Repositories;
using SimpleDemo.Core;
using SimpleDemo.Dapper.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleDemo.Application
{
    public class LbzService: SimpleDemoServiceBase, ILbzService
    {
        private readonly IRepository<Lbz> _lbzRepository;
        private readonly ITestQuery _testQuery;
        private readonly IRepository<TestTable> _testTableRepository;

        public LbzService(IRepository<Lbz> lbzRepository,
                IRepository<TestTable> testTableRepository,
                ITestQuery testQuery
            )
        {
            _lbzRepository = lbzRepository;
            _testTableRepository = testTableRepository;
            _testQuery = testQuery;
        }

        public async Task<IList<Lbz>> GetAll()
        {
            var strLbz = await _testQuery.GetStringAsync(1);
            return await _lbzRepository.GetAllListAsync();
        }

        public async Task<IList<TestTable>> GetTestAll()
        {
            return await _testTableRepository.GetAllListAsync();
        }

    }
}
