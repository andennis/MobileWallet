using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassSiteCertificateConfiguration : EntityTypeConfiguration<PassSiteCertificate>
    {
        public PassSiteCertificateConfiguration(string dbScheme)
        {
            ToTable("PassSiteCertificate", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            HasRequired(x => x.PassSite).WithMany(x => x.Certificates).HasForeignKey(x => x.PassSiteId);
            HasRequired(x => x.PassCertificate).WithMany(x => x.PassSites).HasForeignKey(x => x.PassCertificateId);
        }
    }
}