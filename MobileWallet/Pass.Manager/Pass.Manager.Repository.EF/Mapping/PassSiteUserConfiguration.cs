using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassSiteUserConfiguration : EntityTypeConfiguration<PassSiteUser>
    {
        public PassSiteUserConfiguration(string dbScheme)
        {
            ToTable("PassSiteUser", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            HasRequired(x => x.User).WithMany(x => x.PassSites).HasForeignKey(x => x.UserId);
            HasRequired(x => x.PassSite).WithMany(x => x.Users).HasForeignKey(x => x.PassSiteId);
        }
    }
}
