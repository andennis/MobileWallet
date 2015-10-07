using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassSiteConfiguration : EntityTypeConfiguration<PassSite>
    {
        public PassSiteConfiguration(string dbScheme)
        {
            ToTable("PassSite", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.Name).IsRequired().HasMaxLength(512);
        }
    }
}
