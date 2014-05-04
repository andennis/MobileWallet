using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using CertificateStorage.Repository.Core.Entities;

namespace CertificateStorage.Repository.EF
{
    public class CertificateStorageDbContext : DbContext
    {
        public const string DbScheme = "cer";

        public CertificateStorageDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
            //Database.SetInitializer<PassCertificateStorageDbContext>(null);
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Certificate> FolderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificate>().ToTable("Certificate", DbScheme);
            modelBuilder.Entity<Certificate>().Property(x => x.Name).HasMaxLength(512)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Certificate_Name"){IsUnique = true}));
            modelBuilder.Entity<Certificate>().Property(x => x.Password).HasMaxLength(512);
            modelBuilder.Entity<Certificate>().Property(x => x.FileId).IsRequired();
        }
    }
}
