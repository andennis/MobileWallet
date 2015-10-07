using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassProjectFieldConfiguration: EntityTypeConfiguration<PassProjectField>
    {
        public PassProjectFieldConfiguration(string dbScheme)
        {
            ToTable("PassProjectField", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.Name).IsRequired().HasMaxLength(512);
            Property(x => x.DefaultLabel).HasMaxLength(128);
            HasRequired(x => x.PassProject).WithMany(x => x.PassFields).HasForeignKey(x => x.PassProjectId).WillCascadeOnDelete(false);
        }
    }
}
