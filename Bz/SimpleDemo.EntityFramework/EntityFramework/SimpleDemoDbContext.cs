using Bz.EntityFramework;
using SimpleDemo.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDemo.EntityFramework
{
    public class SimpleDemoDbContext : BzDbContext
    {
        public virtual IDbSet<Lbz> Lbzs { get; set; }

        public SimpleDemoDbContext()
            : base("Default")
        {

        }

        public SimpleDemoDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        
        /// <summary>
        /// 用于单元测试fake/mock个Connection
        /// </summary>
        /// <param name="dbConnection"></param>
        public SimpleDemoDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ConfigurationMapperWithNamespace("SimpleDemo.EntityFramework.Mapping");
            base.OnModelCreating(modelBuilder);
        }
    }
}
