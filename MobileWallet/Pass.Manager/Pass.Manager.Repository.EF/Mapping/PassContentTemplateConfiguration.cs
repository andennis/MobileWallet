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
            Property(x => x.Name).IsRequired().HasMaxLength(DbFieldSettings.FieldLenName);
            Property(x => x.Description).IsRequired();
            Property(x => x.OrganizationName).IsRequired().HasMaxLength(DbFieldSettings.FieldLenName);
            HasRequired(x => x.PassProject).WithMany(x => x.PassContentTemplates).HasForeignKey(x => x.PassProjectId).WillCascadeOnDelete(false);
        }
    }
}
