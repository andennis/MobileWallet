using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Common.Repository.EF;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF
{
    public class PassManagerDbContext : DbContextBase
    {
        private const int FieldLenName = 512;

        public PassManagerDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<PassManagerDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public override string DbScheme { get { return "pm"; } }


        //TODO it might be rempved
        public DbSet<PassSite> PassSites { get; set; }
        public DbSet<PassProject> PassProjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PassSiteUser> PassSiteUsers { get; set; }
        public DbSet<PassCertificate> PassCertificates { get; set; }
        public DbSet<PassSiteCertificate> PassSiteCertificates { get; set; }
        public DbSet<PassProjectField> PassFields { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //PassSite
            modelBuilder.Entity<PassSite>().ToTable("PassSite", DbScheme);
            modelBuilder.Entity<PassSite>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassSite>().Property(x => x.Name).IsRequired().HasMaxLength(FieldLenName);

            //PassProject
            modelBuilder.Entity<PassProject>().ToTable("PassProject", DbScheme);
            modelBuilder.Entity<PassProject>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassProject>().Property(x => x.Name).IsRequired().HasMaxLength(FieldLenName);
            modelBuilder.Entity<PassProject>().HasRequired(x => x.PassSite).WithMany(x => x.Projects).HasForeignKey(x => x.PassSiteId);
            modelBuilder.Entity<PassProject>().HasRequired(x => x.PassCertificate).WithMany().HasForeignKey(x => x.PassCertificateId).WillCascadeOnDelete(false);

            //User
            modelBuilder.Entity<User>().ToTable("User", DbScheme);
            modelBuilder.Entity<User>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<User>().Property(x => x.UserName).IsRequired().HasMaxLength(FieldLenName)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_User_Name") { IsUnique = true }));
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasMaxLength(FieldLenName);
            modelBuilder.Entity<User>().Property(x => x.LastName).HasMaxLength(FieldLenName);
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(512);

            //PassSiteUser
            modelBuilder.Entity<PassSiteUser>().ToTable("PassSiteUser", DbScheme);
            modelBuilder.Entity<PassSiteUser>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassSiteUser>().HasRequired(x => x.User).WithMany(x => x.PassSites).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<PassSiteUser>().HasRequired(x => x.PassSite).WithMany(x => x.Users).HasForeignKey(x => x.PassSiteId);

            //PassCertificate
            modelBuilder.Entity<PassCertificate>().ToTable("PassCertificate", DbScheme);
            modelBuilder.Entity<PassCertificate>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassCertificate>().Property(x => x.Name).IsRequired().HasMaxLength(FieldLenName);

            //PassCertificateApple
            modelBuilder.Entity<PassCertificateApple>().ToTable("PassCertificateApple", DbScheme);
            modelBuilder.Entity<PassCertificateApple>().Property(x => x.TeamId).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<PassCertificateApple>().Property(x => x.PassTypeId).IsRequired().HasMaxLength(128);

            //PassSiteCertificate
            modelBuilder.Entity<PassSiteCertificate>().ToTable("PassSiteCertificate", DbScheme);
            modelBuilder.Entity<PassSiteCertificate>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassSiteCertificate>().HasRequired(x => x.PassSite).WithMany(x => x.Certificates).HasForeignKey(x => x.PassSiteId);
            modelBuilder.Entity<PassSiteCertificate>().HasRequired(x => x.PassCertificate).WithMany(x => x.PassSites).HasForeignKey(x => x.PassCertificateId);

            //PassProjectField
            modelBuilder.Entity<PassProjectField>().ToTable("PassProjectField", DbScheme);
            modelBuilder.Entity<PassProjectField>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassProjectField>().Property(x => x.Name).IsRequired().HasMaxLength(FieldLenName);
            modelBuilder.Entity<PassProjectField>().HasRequired(x => x.PassProject).WithMany(x => x.PassFields).HasForeignKey(x => x.PassProjectId).WillCascadeOnDelete(false);

            //PassContentTemplate
            modelBuilder.Entity<PassContentTemplate>().ToTable("PassContentTemplate", DbScheme);
            modelBuilder.Entity<PassContentTemplate>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassContentTemplate>().Property(x => x.Name).IsRequired().HasMaxLength(FieldLenName);
            modelBuilder.Entity<PassContentTemplate>().Property(x => x.Description).IsRequired();
            modelBuilder.Entity<PassContentTemplate>().Property(x => x.OrganizationName).IsRequired().HasMaxLength(FieldLenName);
            modelBuilder.Entity<PassContentTemplate>().HasRequired(x => x.PassProject).WithMany(x => x.PassContentTemplates).HasForeignKey(x => x.PassProjectId).WillCascadeOnDelete(false);

            //PassContentTemplateField
            modelBuilder.Entity<PassContentTemplateField>().ToTable("PassContentTemplateField", DbScheme);
            modelBuilder.Entity<PassContentTemplateField>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassContentTemplateField>().Property(x => x.AttributedValue).HasMaxLength(128);
            modelBuilder.Entity<PassContentTemplateField>().Property(x => x.ChangeMessage).HasMaxLength(128);
            modelBuilder.Entity<PassContentTemplateField>().Property(x => x.Label).HasMaxLength(128);
            modelBuilder.Entity<PassContentTemplateField>().HasRequired(x => x.PassContentTemplate).WithMany(x => x.PassContentTemplateFields).HasForeignKey(x => x.PassContentTemplateId);
            modelBuilder.Entity<PassContentTemplateField>().HasRequired(x => x.PassProjectField).WithMany().HasForeignKey(x => x.PassProjectFieldId);

            //PassBarcode
            modelBuilder.Entity<PassBarcode>().ToTable("PassBarcode", DbScheme);
            modelBuilder.Entity<PassBarcode>().HasKey(x => x.PassContentTemplateId);
            modelBuilder.Entity<PassBarcode>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassBarcode>().Property(x => x.Format).IsRequired();
            modelBuilder.Entity<PassBarcode>().Property(x => x.Message).IsRequired().HasMaxLength(64);
            modelBuilder.Entity<PassBarcode>().Property(x => x.MessageEncoding).HasMaxLength(32);
            modelBuilder.Entity<PassBarcode>().HasRequired(x => x.PassContentTemplate).WithOptional(x => x.Barcode);

            //PassBeacon
            modelBuilder.Entity<PassBeacon>().ToTable("PassBeacon", DbScheme);
            modelBuilder.Entity<PassBeacon>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassBeacon>().Property(x => x.Name).IsRequired().HasMaxLength(FieldLenName);
            modelBuilder.Entity<PassBeacon>().Property(x => x.ProximityUuid).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<PassBeacon>().HasRequired(x => x.PassContentTemplate).WithMany(x => x.Beacons).HasForeignKey(x => x.PassContentTemplateId);

            //PassLocation
            modelBuilder.Entity<PassLocation>().ToTable("PassLocation", DbScheme);
            modelBuilder.Entity<PassLocation>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassLocation>().Property(x => x.Name).IsRequired().HasMaxLength(FieldLenName);
            modelBuilder.Entity<PassLocation>().Property(x => x.Latitude).IsRequired();
            modelBuilder.Entity<PassLocation>().Property(x => x.Longitude).IsRequired();
            modelBuilder.Entity<PassLocation>().HasRequired(x => x.PassContentTemplate).WithMany(x => x.Locations).HasForeignKey(x => x.PassContentTemplateId);

            //PassImage
            modelBuilder.Entity<PassImage>().ToTable("PassImage", DbScheme);
            modelBuilder.Entity<PassImage>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassImage>().HasRequired(x => x.PassContentTemplate).WithMany(x => x.PassImages).HasForeignKey(x => x.PassContentTemplateId);
            modelBuilder.Entity<PassImage>().Ignore(x => x.ImageFile);
            modelBuilder.Entity<PassImage>().Ignore(x => x.ImageFile2x);

            //PassContent
            modelBuilder.Entity<PassContent>().ToTable("PassContent", DbScheme);
            modelBuilder.Entity<PassContent>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassContent>().Property(x => x.AuthToken).IsRequired().HasMaxLength(64).IsUnicode(false);
            modelBuilder.Entity<PassContent>().Property(x => x.SerialNumber).IsRequired().HasMaxLength(64).IsUnicode(false);
            modelBuilder.Entity<PassContent>().HasRequired(x => x.PassContentTemplate).WithMany().HasForeignKey(x => x.PassContentTemplateId).WillCascadeOnDelete(false);

            //PassContentFieldValue
            modelBuilder.Entity<PassContentField>().ToTable("PassContentField", DbScheme);
            modelBuilder.Entity<PassContentField>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassContentField>().HasRequired(x => x.PassProjectField).WithMany().HasForeignKey(x => x.PassProjectFieldId).WillCascadeOnDelete(false);
            modelBuilder.Entity<PassContentField>().Property(x => x.FieldLabel).HasMaxLength(512);
            modelBuilder.Entity<PassContentField>().Property(x => x.FieldValue).HasMaxLength(512);
            modelBuilder.Entity<PassContentField>().HasRequired(x => x.PassContent).WithMany(x => x.Fields).HasForeignKey(x => x.PassContentId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
