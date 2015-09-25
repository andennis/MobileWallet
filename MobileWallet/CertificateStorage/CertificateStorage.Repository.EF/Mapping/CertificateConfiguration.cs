using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using CertificateStorage.Repository.Core.Entities;

namespace CertificateStorage.Repository.EF.Mapping
{
    public class CertificateConfiguration : EntityTypeConfiguration<Certificate>
    {
        public CertificateConfiguration(string dbScheme)
        {
            ToTable("Certificate", dbScheme);
            Property(x => x.Name).HasMaxLength(256).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Certificate_Name") { IsUnique = true }));
            Property(x => x.Password).HasMaxLength(512);
            Property(x => x.FileId).IsRequired();
        }
    }
}
