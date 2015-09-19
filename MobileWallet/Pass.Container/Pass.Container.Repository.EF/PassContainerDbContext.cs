using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Common.Repository.EF;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF
{
    public class PassContainerDbContext : DbContextBase
    {
        private const int FieldLenPassTypeId = 128;
        private const int FieldLenName = 512;

        public PassContainerDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<PassContainerDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public override string DbScheme { get { return "pscn"; } }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //PassTemplate
            modelBuilder.Entity<PassTemplate>().ToTable("PassTemplate", DbScheme);
            modelBuilder.Entity<PassTemplate>().Property(x => x.Name).IsRequired().HasMaxLength(FieldLenName);
            modelBuilder.Entity<PassTemplate>().Property(x => x.Version).IsConcurrencyToken();

            //PassField
            modelBuilder.Entity<PassField>().ToTable("PassField", DbScheme);
            modelBuilder.Entity<PassField>().HasRequired(x => x.Template).WithMany(x => x.PassFields).HasForeignKey(x => x.PassTemplateId);
            modelBuilder.Entity<PassField>().Property(x => x.Name).IsRequired().HasMaxLength(FieldLenName);
            modelBuilder.Entity<PassField>().Property(x => x.Version).IsConcurrencyToken();

            //Pass
            modelBuilder.Entity<Core.Entities.Pass>().ToTable("Pass", DbScheme);
            modelBuilder.Entity<Core.Entities.Pass>().HasRequired(x => x.Template).WithMany(x => x.Passes).HasForeignKey(x => x.PassTemplateId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Core.Entities.Pass>().Property(x => x.AuthToken).IsRequired().HasMaxLength(64).IsUnicode(false);
            modelBuilder.Entity<Core.Entities.Pass>().Property(x => x.Version).IsConcurrencyToken();

            //PassNative
            modelBuilder.Entity<PassNative>().ToTable("PassNative", DbScheme);
            modelBuilder.Entity<PassNative>().HasRequired(x => x.Pass).WithMany(x => x.NativePasses).HasForeignKey(x => x.PassId);
            modelBuilder.Entity<PassNative>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassNative>().Property(x => x.SerialNumber).IsRequired().HasMaxLength(64).IsUnicode(false);
            modelBuilder.Entity<PassNative>().HasRequired(x => x.PassTemplateNative).WithMany().HasForeignKey(x => x.PassTemplateNativeId);

            //PassApple
            modelBuilder.Entity<PassApple>().ToTable("PassApple", DbScheme);
            modelBuilder.Entity<PassApple>().Property(x => x.PassTypeId).IsRequired().HasMaxLength(FieldLenPassTypeId).IsUnicode(false);

            //PassFieldValue
            modelBuilder.Entity<PassFieldValue>().ToTable("PassFieldValue", DbScheme);
            modelBuilder.Entity<PassFieldValue>().HasRequired(x => x.Pass).WithMany(x => x.FieldValues).HasForeignKey(x => x.PassId);
            modelBuilder.Entity<PassFieldValue>().HasRequired(x => x.PassField).WithMany(x => x.FieldValues).HasForeignKey(x => x.PassFieldId).WillCascadeOnDelete(false);
            modelBuilder.Entity<PassFieldValue>().Property(x => x.Label).HasMaxLength(512);
            modelBuilder.Entity<PassFieldValue>().Property(x => x.Value).HasMaxLength(512);
            modelBuilder.Entity<PassFieldValue>().Property(x => x.Version).IsConcurrencyToken();

            //PassTemplateNative
            modelBuilder.Entity<PassTemplateNative>().ToTable("PassTemplateNative", DbScheme);
            modelBuilder.Entity<PassTemplateNative>().HasRequired(x => x.Template).WithMany(x => x.NativeTemplates).HasForeignKey(x => x.PassTemplateId);

            //PassTemplateApple
            modelBuilder.Entity<PassTemplateApple>().ToTable("PassTemplateApple", DbScheme);
            modelBuilder.Entity<PassTemplateApple>().Property(x => x.PassTypeId).IsRequired().HasMaxLength(FieldLenPassTypeId).IsUnicode(false);

            //Client device registrations
            modelBuilder.Entity<ClientDevice>().ToTable("ClientDevice", DbScheme);
            modelBuilder.Entity<ClientDevice>().Property(x => x.DeviceId).IsRequired().HasMaxLength(64).IsUnicode(false);

            modelBuilder.Entity<ClientDeviceApple>().ToTable("ClientDeviceApple", DbScheme);
            modelBuilder.Entity<ClientDeviceApple>().Property(x => x.PushToken).IsRequired().HasMaxLength(64).IsUnicode(false);

            //Registration
            modelBuilder.Entity<Registration>().ToTable("Registration", DbScheme);
            modelBuilder.Entity<Registration>().HasKey(x => new { x.ClientDeviceId, x.PassId });
            modelBuilder.Entity<Registration>().HasRequired(x => x.Pass).WithMany(x => x.PassRegistrations).HasForeignKey(x => x.PassId);
            modelBuilder.Entity<Registration>().HasRequired(x => x.ClientDevice).WithMany(x => x.PassRegistrations).HasForeignKey(x => x.ClientDeviceId);

            //SequenceCounter
            modelBuilder.Entity<SequenceCounter>().ToTable("SequenceCounter", DbScheme);
            modelBuilder.Entity<SequenceCounter>().Property(x => x.Name).HasMaxLength(50)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_SequenceCounter_Name") { IsUnique = true })); ;

            base.OnModelCreating(modelBuilder);
        }
    }
}
