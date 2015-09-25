using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    public class ClientDeviceAppleConfiguration : EntityTypeConfiguration<ClientDeviceApple>
    {
        public ClientDeviceAppleConfiguration(string dbScheme)
        {
            ToTable("ClientDeviceApple", dbScheme);
            Property(x => x.PushToken).IsRequired().HasMaxLength(64).IsUnicode(false);
        }
    }
}
