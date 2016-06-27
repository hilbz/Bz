using SimpleDemo.Core;
using SimpleDemo.EntityFramework.Mapping;

namespace SimpleSecond.EntityFramework.Mapping
{
    public class TestTableMap : BzEntityTypeConfiguration<TestTable>
    {
        public TestTableMap()
        {
            ToTable("TestTable");
            HasKey(t => t.Id);
            Property(t => t.Name).HasMaxLength(50).IsUnicode(false).IsRequired();
            Property(t => t.CreateDate).HasColumnName("CreateDate").IsRequired();
        }
    }
}
