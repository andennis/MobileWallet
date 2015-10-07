using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Common.Repository.EF;
using Pass.Manager.Core.Entities;
using Pass.Manager.Repository.EF.Mapping;

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


        //TODO it might be removed
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
            modelBuilder.Configurations.Add<PassSite>(new PassSiteConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassProject>(new PassProjectConfiguration(DbScheme)); 
            modelBuilder.Configurations.Add<User>(new UserConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassSiteUser>(new PassSiteUserConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassCertificate>(new PassCertificateConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassCertificateApple>(new PassCertificateAppleConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassSiteCertificate>(new PassSiteCertificateConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassProjectField>(new PassProjectFieldConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassContentTemplate>(new PassContentTemplateConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassContentTemplateField>(new PassContentTemplateFieldConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassBarcode>(new PassBarcodeConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassBeacon>(new PassBeaconConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassLocation>(new PassLocationConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassImage>(new PassImageConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassContent>(new PassContentConfiguration(DbScheme));
            modelBuilder.Configurations.Add<PassContentField>(new PassContentFieldConfiguration(DbScheme));
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
