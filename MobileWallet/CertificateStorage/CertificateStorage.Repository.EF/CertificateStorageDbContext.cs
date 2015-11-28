using System.Data.Entity;
using CertificateStorage.Repository.Core.Entities;
using CertificateStorage.Repository.EF.Mapping;
using Common.Repository.EF;

namespace CertificateStorage.Repository.EF
{
    public class CertificateStorageDbContext : DbContextBase
    {
        public CertificateStorageDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<CertificateStorageDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public override string DbScheme { get { return "cer"; } } 

        //public DbSet<Certificate> FolderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Certificate>(new CertificateConfiguration(DbScheme));
        }
    }
}
