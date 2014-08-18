using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Pass.Manager.Repository.Core.Entities;

namespace Pass.Manager.Repository.EF
{
    public class PassManagerDbContext : DbContext
    {
        private const string DbScheme = "pm";
        private const int FieldLenName = 512;

        public PassManagerDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<PassManagerDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<PassSite> PassSites { get; set; }
        public DbSet<PassProject> PassProjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PassSiteUser> PassSiteUsers { get; set; }
        public DbSet<PassCertificate> PassCertificates { get; set; }
        public DbSet<PassSiteCertificate> PassSiteCertificates { get; set; }

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

            //User
            modelBuilder.Entity<User>().ToTable("User", DbScheme);
            modelBuilder.Entity<User>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<User>().Property(x => x.UserName).IsRequired().HasMaxLength(FieldLenName)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_User_Name") { IsUnique = true })); 

            //PassSiteUser
            modelBuilder.Entity<PassSiteUser>().ToTable("PassSiteUser", DbScheme);
            modelBuilder.Entity<PassSiteUser>().HasKey(x => new {x.PassSiteId, x.UserId});
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
            modelBuilder.Entity<PassSiteCertificate>().HasKey(x => new {x.PassSiteId, x.PassCertificateId});
            modelBuilder.Entity<PassSiteCertificate>().Property(x => x.Version).IsConcurrencyToken();
            modelBuilder.Entity<PassSiteCertificate>().HasRequired(x => x.PassSite).WithMany(x => x.Certificates).HasForeignKey(x => x.PassSiteId);
            modelBuilder.Entity<PassSiteCertificate>().HasRequired(x => x.PassCertificate).WithMany(x => x.PassSites).HasForeignKey(x => x.PassCertificateId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
