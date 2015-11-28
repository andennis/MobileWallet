using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassBeaconConfiguration : EntityTypeConfiguration<PassBeacon>
    {
        public PassBeaconConfiguration(string dbScheme)
        {
            ToTable("PassBeacon", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.Name).IsRequired().HasMaxLength(DbFieldSettings.FieldLenName);
            Property(x => x.ProximityUuid).IsRequired().HasMaxLength(128);
            HasRequired(x => x.PassContentTemplate).WithMany(x => x.Beacons).HasForeignKey(x => x.PassContentTemplateId);
        }
    }
}
