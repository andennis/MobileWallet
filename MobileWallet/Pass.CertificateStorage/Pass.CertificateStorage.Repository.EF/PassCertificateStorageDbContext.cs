using System.Data.Entity;
using Pass.CertificateStorage.Repository.Core.Entities;

namespace Pass.CertificateStorage.Repository.EF
{
    public class PassCertificateStorageDbContext : DbContext
    {
        public const string DbScheme = "cer";

        public PassCertificateStorageDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<PassCertificateStorageDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Certificate> FolderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificate>().ToTable("Certificate", DbScheme);
            modelBuilder.Entity<Certificate>().Property(x => x.Name).IsRequired().HasMaxLength(400);
        }
    }
}
