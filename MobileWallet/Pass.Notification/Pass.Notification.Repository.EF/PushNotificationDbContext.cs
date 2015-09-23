using System.Data.Entity;
using Common.Repository.EF;
using Pass.Notification.Repository.Core.Entities;

namespace Pass.Notification.Repository.EF
{
    public sealed class PushNotificationDbContext: DbContextBase
    {
        public PushNotificationDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<PushNotificationDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public override string DbScheme { get { return "pn"; } }

        public DbSet<PushNotificationItem> PushNotificationItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PushNotificationItem>().ToTable("PushNotificationItem", DbScheme);
            modelBuilder.Entity<PushNotificationItem>().Property(x => x.CertificateStorageId).IsRequired();
            modelBuilder.Entity<PushNotificationItem>().Property(x => x.PushTockenId).IsRequired();
            modelBuilder.Entity<PushNotificationItem>().Property(x => x.Version).IsConcurrencyToken();
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
