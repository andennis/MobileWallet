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
            //modelBuilder.Entity<PassProject>().HasRequired(x => x.PassCertificate).WithRequiredDependent().WillCascadeOnDelete(false);

            //User
            modelBuilder.Entity<User>().ToTable("User", DbScheme);
            modelBuilder.Entity<User>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<User>().Property(x => x.UserName).IsRequired().HasMaxLength(FieldLenName)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_User_Name") { IsUnique = true })); 

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

            modelBuilder.Entity<PassContentTemplate>().ToTable("PassContentTemplate", DbScheme);
            modelBuilder.Entity<PassContentTemplate>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassContentTemplate>().Property(x => x.Description).IsRequired();
            modelBuilder.Entity<PassContentTemplate>().Property(x => x.OrganizationName).IsRequired().HasMaxLength(FieldLenName);
            modelBuilder.Entity<PassContentTemplate>().HasRequired(x => x.PassProject).WithMany(x => x.PassContentTemplates).HasForeignKey(x => x.PassProjectId).WillCascadeOnDelete(false);

            modelBuilder.Entity<PassBeacon>().ToTable("PassBeacon", DbScheme);
            modelBuilder.Entity<PassBeacon>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassBeacon>().Property(x => x.ProximityUuid).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<PassBeacon>().HasRequired(x => x.PassContentTemplate).WithMany(x => x.Beacons).HasForeignKey(x => x.PassContentTemplateId);

            modelBuilder.Entity<PassLocation>().ToTable("PassLocation", DbScheme);
            modelBuilder.Entity<PassLocation>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassLocation>().Property(x => x.Latitude).IsRequired();
            modelBuilder.Entity<PassLocation>().Property(x => x.Longitude).IsRequired();
            modelBuilder.Entity<PassLocation>().HasRequired(x => x.PassContentTemplate).WithMany(x => x.Locations).HasForeignKey(x => x.PassContentTemplateId);

            modelBuilder.Entity<PassContentTemplateField>().ToTable("PassContentTemplateField", DbScheme);
            modelBuilder.Entity<PassContentTemplateField>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassContentTemplateField>().Property(x => x.AttributedValue).HasMaxLength(128);
            modelBuilder.Entity<PassContentTemplateField>().Property(x => x.ChangeMessage).HasMaxLength(128);
            modelBuilder.Entity<PassContentTemplateField>().Property(x => x.Label).HasMaxLength(128);
            modelBuilder.Entity<PassContentTemplateField>().HasRequired(x => x.PassContentTemplate).WithMany(x => x.PassContentTemplateFields).HasForeignKey(x => x.PassContentTemplateId);
            modelBuilder.Entity<PassContentTemplateField>().HasRequired(x => x.PassProjectField).WithMany().HasForeignKey(x => x.PassProjectFieldId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
