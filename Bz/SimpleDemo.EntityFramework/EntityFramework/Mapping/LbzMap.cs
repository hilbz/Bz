using SimpleDemo.Core;
namespace SimpleDemo.EntityFramework.Mapping
{
    public class LbzMap : BzEntityTypeConfiguration<Lbz>
    {
        public LbzMap()
        {
            ToTable("Lbz");
            HasKey(t => t.Id);
            Property(t => t.Name).HasMaxLength(50).IsUnicode(false).IsRequired();
            Property(t => t.CreateDate).HasColumnName("CreateDate").IsRequired();
        }
    }
}
