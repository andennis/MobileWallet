using System.Data.Entity.ModelConfiguration;
using Pass.Notification.Repository.Core.Entities;

namespace Pass.Notification.Repository.EF.Mapping
{
    public class PushNotificationItemConfiguration : EntityTypeConfiguration<PushNotificationItem>
    {
        public PushNotificationItemConfiguration(string dbScheme)
        {
           ToTable("PushNotificationItem", dbScheme);
           Property(x => x.CertificateStorageId).IsRequired();
           Property(x => x.PushTockenId).IsRequired();
        }
    }
}

