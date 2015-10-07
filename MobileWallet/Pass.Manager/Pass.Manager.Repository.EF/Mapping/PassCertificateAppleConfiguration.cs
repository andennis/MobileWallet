using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassCertificateAppleConfiguration : EntityTypeConfiguration<PassCertificateApple>
    {
        public PassCertificateAppleConfiguration(string dbScheme)
        {
            ToTable("PassCertificateApple", dbScheme);
            Property(x => x.TeamId).IsRequired().HasMaxLength(128);
            Property(x => x.PassTypeId).IsRequired().HasMaxLength(128);
        }
    }
}