using System.Data.Entity;
using Common.Repository.EF;
using Pass.Notification.Repository.Core.Entities;
using Pass.Notification.Repository.EF.Mapping;

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
            modelBuilder.Configurations.Add<PushNotificationItem>(new PushNotificationItemConfiguration(DbScheme));
            base.OnModelCreating(modelBuilder);
        }
    }
}
