using SimpleDemo.Application;
using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace SimpleDemo.Tests
{
    public class LbzService_Tests : AppTestBase
    {
        private readonly ILbzService _lbzService;

        public LbzService_Tests()
        {
            _lbzService = Resolve<ILbzService>();
        }

        [Fact]
        public async Task GetLbz_Test()
        {
            var list =await _lbzService.GetAll();
            list.Count.ShouldBe(1);
        }
    }
}
