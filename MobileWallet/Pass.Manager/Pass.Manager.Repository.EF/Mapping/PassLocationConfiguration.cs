using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassLocationConfiguration : EntityTypeConfiguration<PassLocation>
    {
        public PassLocationConfiguration(string dbScheme)
        {
            ToTable("PassLocation", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.Name).IsRequired().HasMaxLength(DbFieldSettings.FieldLenName);
            Property(x => x.Latitude).IsRequired();
            Property(x => x.Longitude).IsRequired();
            HasRequired(x => x.PassContentTemplate).WithMany(x => x.Locations).HasForeignKey(x => x.PassContentTemplateId);
        }
    }
}
