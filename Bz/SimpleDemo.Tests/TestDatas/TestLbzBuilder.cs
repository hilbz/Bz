using SimpleDemo.Core;
using SimpleDemo.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDemo.Tests.TestDatas
{
    public class TestLbzBuilder
    {
        private readonly SimpleDemoDbContext _context;

        public TestLbzBuilder(SimpleDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLbz("Lbz_Ting");
        }

        private Lbz CreateLbz(string name)
        {
            var lbz = _context.Lbzs.Add(new Lbz(){Name = name});
            _context.SaveChanges();
            return lbz;            
        }
    }
}
