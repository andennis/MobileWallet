using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    public class PassTemplateConfiguration : EntityTypeConfiguration<PassTemplate>
    {
        public PassTemplateConfiguration(string dbScheme)
        {
            ToTable("PassTemplate", dbScheme);
            Property(x => x.Name).IsRequired().HasMaxLength(512);
            Property(x => x.Version).IsConcurrencyToken();
        }
    }
}
