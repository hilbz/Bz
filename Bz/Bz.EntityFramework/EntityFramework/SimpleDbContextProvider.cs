using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.EntityFramework
{
    public sealed class SimpleDbContextProvider<TDbContext>:IDbContextProvider<TDbContext>
        where TDbContext:DbContext
    {
        public TDbContext DbContext { get; private set; }

        public SimpleDbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
