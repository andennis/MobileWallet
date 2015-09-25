using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    public class ClientDeviceConfiguration : EntityTypeConfiguration<ClientDevice>
    {
        public ClientDeviceConfiguration(string dbScheme)
        {
            ToTable("ClientDevice", dbScheme);
            Property(x => x.DeviceId).IsRequired().HasMaxLength(64).IsUnicode(false);
        }
    }
}
