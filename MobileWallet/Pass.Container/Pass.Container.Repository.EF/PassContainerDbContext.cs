using System.Data.Entity;
using PassEntities = Pass.Container.Core.Entities;

namespace Pass.Container.Repository.EF
{
    public class PassContainerDbContext : DbContext
    {
        public const string DbScheme = "pscn";

        public PassContainerDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<FileStorageDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<PassEntities.Pass> Passes { get; set; }
        public DbSet<PassEntities.PassTemplate> PassTemplates { get; set; }
        public DbSet<PassEntities.PassField> PassFields { get; set; }
        public DbSet<PassEntities.PassFieldValue> PassFieldValues { get; set; }

        public DbSet<PassEntities.ClientDevice> ClientDevices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Pass
            modelBuilder.Entity<PassEntities.PassTemplate>().ToTable("PassTemplate", DbScheme);
            modelBuilder.Entity<PassEntities.PassTemplate>().Property(x => x.Name).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<PassEntities.PassTemplate>().Property(x => x.Version).IsConcurrencyToken();

            modelBuilder.Entity<PassEntities.Pass>().ToTable("Pass", DbScheme);
            modelBuilder.Entity<PassEntities.Pass>().HasRequired(x => x.Template).WithMany(x => x.Passes).Map(x => x.MapKey("PassTemplateId"));
            modelBuilder.Entity<PassEntities.Pass>().Property(x => x.AuthToken).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<PassEntities.Pass>().Property(x => x.SerialNumber).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<PassEntities.Pass>().Property(x => x.Version).IsConcurrencyToken();

            modelBuilder.Entity<PassEntities.PassApple>().ToTable("PassApple", DbScheme);
            modelBuilder.Entity<PassEntities.PassApple>().Property(x => x.PassTypeId).IsRequired().HasMaxLength(400);

            modelBuilder.Entity<PassEntities.PassField>().ToTable("PassField", DbScheme);
            modelBuilder.Entity<PassEntities.PassField>().HasRequired(x => x.Template).WithMany(x => x.PassFields).Map(x => x.MapKey("PassTemplateId"));
            modelBuilder.Entity<PassEntities.PassField>().Property(x => x.Name).IsRequired().HasMaxLength(400);
            modelBuilder.Entity<PassEntities.PassField>().Property(x => x.Version).IsConcurrencyToken();

            modelBuilder.Entity<PassEntities.PassFieldValue>().ToTable("PassFieldValue", DbScheme);
            modelBuilder.Entity<PassEntities.PassFieldValue>().HasRequired(x => x.Pass).WithMany(x => x.FieldValues).Map(x => x.MapKey("PassId"));
            modelBuilder.Entity<PassEntities.PassFieldValue>().HasRequired(x => x.PassField).WithRequiredDependent().Map(x => x.MapKey("PassFieldId"));
            modelBuilder.Entity<PassEntities.PassFieldValue>().Property(x => x.Value).IsOptional().HasMaxLength(400);
            modelBuilder.Entity<PassEntities.PassFieldValue>().Property(x => x.Version).IsConcurrencyToken();

            modelBuilder.Entity<PassEntities.PassTemplateNative>().ToTable("PassTemplateNative", DbScheme);
            modelBuilder.Entity<PassEntities.PassTemplateNative>().HasRequired(x => x.Template).WithMany(x => x.NativeTemplates).Map(x => x.MapKey("PassTemplateId"));

            modelBuilder.Entity<PassEntities.PassTemplateApple>().ToTable("PassTemplateApple", DbScheme);
            modelBuilder.Entity<PassEntities.PassTemplateApple>().Property(x => x.PassTypeId).IsRequired().HasMaxLength(400);

            //Client device registrations
            modelBuilder.Entity<PassEntities.ClientDevice>().ToTable("ClientDevice", DbScheme);
            modelBuilder.Entity<PassEntities.ClientDevice>().Property(x => x.DeviceId).IsRequired().HasMaxLength(400);

            modelBuilder.Entity<PassEntities.ClientDeviceApple>().ToTable("ClientDeviceApple", DbScheme);
            modelBuilder.Entity<PassEntities.ClientDeviceApple>().Property(x => x.PushToken).IsRequired().HasMaxLength(400);

            modelBuilder.Entity<PassEntities.Registration>().ToTable("Registration", DbScheme);
            modelBuilder.Entity<PassEntities.Registration>().HasKey(x => new { x.ClientDeviceId, x.PassId });
            modelBuilder.Entity<PassEntities.Registration>().HasRequired(x => x.Pass).WithMany(x => x.PassRegistrations);
            modelBuilder.Entity<PassEntities.Registration>().HasRequired(x => x.ClientDevice).WithMany(x => x.PassRegistrations);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
