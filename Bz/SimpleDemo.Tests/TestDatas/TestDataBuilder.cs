using EntityFramework.DynamicFilters;
using SimpleDemo.EntityFramework;

namespace SimpleDemo.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly SimpleDemoDbContext _context;

        public TestDataBuilder(SimpleDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();
            new TestLbzBuilder(_context).Create();
            _context.SaveChanges();
        }
    }
}
