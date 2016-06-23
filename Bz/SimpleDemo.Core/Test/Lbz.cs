using Bz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDemo.Core
{
    [Table("Lbz")]
    public class Lbz:Entity
    {
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
