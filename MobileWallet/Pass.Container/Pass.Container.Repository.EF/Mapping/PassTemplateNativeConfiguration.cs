using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    public class PassTemplateNativeConfiguration : EntityTypeConfiguration<PassTemplateNative>
    {
        public PassTemplateNativeConfiguration(string dbScheme)
        {
            ToTable("PassTemplateNative", dbScheme);
            HasRequired(x => x.Template).WithMany(x => x.NativeTemplates).HasForeignKey(x => x.PassTemplateId);
        }
    }
}
