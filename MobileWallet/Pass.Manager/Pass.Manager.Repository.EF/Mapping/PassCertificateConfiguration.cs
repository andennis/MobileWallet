using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassCertificateConfiguration : EntityTypeConfiguration<PassCertificate>
    {
        public PassCertificateConfiguration(string dbScheme)
        {
            ToTable("PassCertificate", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.Name).IsRequired().HasMaxLength(512);
        }
    }
}
