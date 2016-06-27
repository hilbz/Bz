using Bz.EntityFramework;
using SimpleDemo.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSecond.EntityFramework
{
    public class SimpleSecondDbContext : BzDbContext
    {
        public virtual IDbSet<TestTable> TestTables { get; set; }

        public SimpleSecondDbContext()
            : base("Bz")
        {
        
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ConfigurationMapperWithNamespace("SimpleSecond.EntityFramework.Mapping");
            base.OnModelCreating(modelBuilder);
        }
    }
}
