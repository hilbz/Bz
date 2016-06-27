using Bz.Domain.Repositories;
using SimpleDemo.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleDemo.Application
{
    public class LbzService: SimpleDemoServiceBase, ILbzService
    {
        private readonly IRepository<Lbz> _lbzRepository;
        private readonly IRepository<TestTable> _testTableRepository;

        public LbzService(IRepository<Lbz> lbzRepository,
                IRepository<TestTable> testTableRepository
            )
        {
            _lbzRepository = lbzRepository;
            _testTableRepository = testTableRepository;
        }

        public async Task<IList<Lbz>> GetAll()
        {
            return await _lbzRepository.GetAllListAsync();
        }

        public async Task<IList<TestTable>> GetTestAll()
        {
            return await _testTableRepository.GetAllListAsync();
        }

    }
}
