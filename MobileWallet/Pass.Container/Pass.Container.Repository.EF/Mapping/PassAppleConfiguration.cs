using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    public class PassAppleConfiguration : EntityTypeConfiguration<PassApple>
    {
        public PassAppleConfiguration(string dbScheme)
        {
            ToTable("PassApple", dbScheme);
            Property(x => x.PassTypeId).IsRequired().HasMaxLength(128).IsUnicode(false);
        }
    }
}
