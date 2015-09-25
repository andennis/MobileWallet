using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    public class RegistrationConfiguration : EntityTypeConfiguration<Registration>
    {
        public RegistrationConfiguration(string dbScheme)
        {
            ToTable("Registration", dbScheme);
            HasKey(x => new { x.ClientDeviceId, x.PassId });
            HasRequired(x => x.Pass).WithMany(x => x.PassRegistrations).HasForeignKey(x => x.PassId);
            HasRequired(x => x.ClientDevice).WithMany(x => x.PassRegistrations).HasForeignKey(x => x.ClientDeviceId);
        }
    }
}
