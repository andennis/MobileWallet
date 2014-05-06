using System.Data.Entity;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF
{
    public class PassContainerDbContext : DbContext
    {
        public const string DbScheme = "pscn";

        public PassContainerDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<PassContainerDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Core.Entities.Pass> Passes { get; set; }
        public DbSet<PassTemplate> PassTemplates { get; set; }
        public DbSet<PassTemplateNative> PassTemplateNatives { get; set; }
        public DbSet<PassField> PassFields { get; set; }
        public DbSet<PassFieldValue> PassFieldValues { get; set; }
        public DbSet<ClientDevice> ClientDevices { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Pass
            modelBuilder.Entity<PassTemplate>().ToTable("PassTemplate", DbScheme);
            modelBuilder.Entity<PassTemplate>().Property(x => x.Name).IsRequired().HasMaxLength(512);
            modelBuilder.Entity<PassTemplate>().Property(x => x.Version).IsConcurrencyToken();

            modelBuilder.Entity<PassField>().ToTable("PassField", DbScheme);
            modelBuilder.Entity<PassField>().HasRequired(x => x.Template).WithMany(x => x.PassFields).HasForeignKey(x => x.PassTemplateId);
            modelBuilder.Entity<PassField>().Property(x => x.Name).IsRequired().HasMaxLength(512);
            modelBuilder.Entity<PassField>().Property(x => x.DefaultValue).HasMaxLength(512);
            modelBuilder.Entity<PassField>().Property(x => x.DefaultLabel).HasMaxLength(512);
            modelBuilder.Entity<PassField>().Property(x => x.Version).IsConcurrencyToken();

            modelBuilder.Entity<Core.Entities.Pass>().ToTable("Pass", DbScheme);
            modelBuilder.Entity<Core.Entities.Pass>().HasRequired(x => x.Template).WithMany(x => x.Passes).HasForeignKey(x => x.PassTemplateId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Core.Entities.Pass>().Property(x => x.AuthToken).IsRequired().HasMaxLength(512);
            modelBuilder.Entity<Core.Entities.Pass>().Property(x => x.SerialNumber).IsRequired().HasMaxLength(512);
            //modelBuilder.Entity<Core.Entities.Pass>().Property(x => x.PassTypeIdentifier).IsRequired().HasMaxLength(512);
            modelBuilder.Entity<Core.Entities.Pass>().Property(x => x.Version).IsConcurrencyToken();

            modelBuilder.Entity<PassFieldValue>().ToTable("PassFieldValue", DbScheme);
            modelBuilder.Entity<PassFieldValue>().HasRequired(x => x.Pass).WithMany(x => x.FieldValues).HasForeignKey(x => x.PassId);
            modelBuilder.Entity<PassFieldValue>().HasRequired(x => x.PassField).WithMany(x => x.FieldValues).HasForeignKey(x => x.PassFieldId).WillCascadeOnDelete(false);
            modelBuilder.Entity<PassFieldValue>().Property(x => x.Label).HasMaxLength(512);
            modelBuilder.Entity<PassFieldValue>().Property(x => x.Value).HasMaxLength(512);
            modelBuilder.Entity<PassFieldValue>().Property(x => x.Version).IsConcurrencyToken();

            modelBuilder.Entity<PassTemplateNative>().ToTable("PassTemplateNative", DbScheme);
            modelBuilder.Entity<PassTemplateNative>().HasRequired(x => x.Template).WithMany(x => x.NativeTemplates).HasForeignKey(x => x.PassTemplateId);

            modelBuilder.Entity<PassTemplateApple>().ToTable("PassTemplateApple", DbScheme);
            modelBuilder.Entity<PassTemplateApple>().Property(x => x.PassTypeId).IsRequired().HasMaxLength(512);

            //Client device registrations
            modelBuilder.Entity<ClientDevice>().ToTable("ClientDevice", DbScheme);
            modelBuilder.Entity<ClientDevice>().Property(x => x.DeviceId).IsRequired().HasMaxLength(512);

            modelBuilder.Entity<ClientDeviceApple>().ToTable("ClientDeviceApple", DbScheme);
            modelBuilder.Entity<ClientDeviceApple>().Property(x => x.PushToken).IsRequired().HasMaxLength(512);

            modelBuilder.Entity<Registration>().ToTable("Registration", DbScheme);
            modelBuilder.Entity<Registration>().HasKey(x => new { x.ClientDeviceId, x.PassId });
            modelBuilder.Entity<Registration>().HasRequired(x => x.Pass).WithMany(x => x.PassRegistrations).HasForeignKey(x => x.PassId);
            modelBuilder.Entity<Registration>().HasRequired(x => x.ClientDevice).WithMany(x => x.PassRegistrations).HasForeignKey(x => x.ClientDeviceId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
