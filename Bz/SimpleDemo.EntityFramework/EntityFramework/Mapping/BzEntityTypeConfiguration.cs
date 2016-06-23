using System.Data.Entity.ModelConfiguration;

namespace SimpleDemo.EntityFramework.Mapping
{
    public abstract class BzEntityTypeConfiguration<T>: EntityTypeConfiguration<T>
        where T:class
    {

    }
}
