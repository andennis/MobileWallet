using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    public class PassNativeConfiguration : EntityTypeConfiguration<PassNative>
    {
        public PassNativeConfiguration(string dbScheme)
        {
            ToTable("PassNative", dbScheme);
            HasRequired(x => x.Pass).WithMany(x => x.NativePasses).HasForeignKey(x => x.PassId);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.SerialNumber).IsRequired().HasMaxLength(64).IsUnicode(false);
            HasRequired(x => x.PassTemplateNative).WithMany().HasForeignKey(x => x.PassTemplateNativeId);
        }
    }
}
