using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    public class PassTemplateAppleConfiguration : EntityTypeConfiguration<PassTemplateApple>
    {
        public PassTemplateAppleConfiguration(string dbScheme)
        {
            ToTable("PassTemplateApple", dbScheme);
            Property(x => x.PassTypeId).IsRequired().HasMaxLength(128).IsUnicode(false);
        }
    }
}
