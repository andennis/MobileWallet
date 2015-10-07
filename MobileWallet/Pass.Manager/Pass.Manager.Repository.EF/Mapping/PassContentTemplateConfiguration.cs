using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassContentTemplateConfiguration : EntityTypeConfiguration<PassContentTemplate>
    {
        public PassContentTemplateConfiguration(string dbScheme)
        {
            ToTable("PassContentTemplate", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.Name).IsRequired().HasMaxLength(512);
            Property(x => x.Description).IsRequired();
            Property(x => x.OrganizationName).IsRequired().HasMaxLength(512);
            HasRequired(x => x.PassProject).WithMany(x => x.PassContentTemplates).HasForeignKey(x => x.PassProjectId).WillCascadeOnDelete(false);
        }
    }
}
