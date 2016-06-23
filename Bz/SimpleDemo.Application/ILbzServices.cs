using Bz.Application.Services;
using SimpleDemo.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleDemo.Application
{
    public interface ILbzService: IApplicationService
    {
        Task<IList<Lbz>> GetAll();
    }
}
