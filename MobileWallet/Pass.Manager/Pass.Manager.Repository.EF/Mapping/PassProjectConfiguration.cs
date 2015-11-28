using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassProjectConfiguration : EntityTypeConfiguration<PassProject>
    {
        public PassProjectConfiguration(string dbScheme)
        {
            ToTable("PassProject", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.Name).IsRequired().HasMaxLength(DbFieldSettings.FieldLenName);
            HasRequired(x => x.PassSite).WithMany(x => x.Projects).HasForeignKey(x => x.PassSiteId);
            HasRequired(x => x.PassCertificate).WithMany().HasForeignKey(x => x.PassCertificateId).WillCascadeOnDelete(false);

        }
    }
}
