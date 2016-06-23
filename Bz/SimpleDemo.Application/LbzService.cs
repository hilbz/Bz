using Bz.Domain.Repositories;
using SimpleDemo.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleDemo.Application
{
    public class LbzService: SimpleDemoServiceBase, ILbzService
    {
        private readonly IRepository<Lbz> _lbzRepository;

        public LbzService(IRepository<Lbz> lbzRepository)
        {
            _lbzRepository = lbzRepository;
        }

        public async Task<IList<Lbz>> GetAll()
        {
            return await _lbzRepository.GetAllListAsync();
        }
    }
}
