using EntityFramework.DynamicFilters;

namespace SimpleDemo.EntityFramework.Migrations.Seed
{
    public class InitialDbBuilder
    {
        private readonly SimpleDemoDbContext _context;

        public InitialDbBuilder(SimpleDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //_context.DisableAllFilters();
        }
    }
}
