using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassContentTemplateFieldConfiguration : EntityTypeConfiguration<PassContentTemplateField>
    {
        public PassContentTemplateFieldConfiguration(string dbScheme)
        {
            ToTable("PassContentTemplateField", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.AttributedValue).HasMaxLength(128);
            Property(x => x.ChangeMessage).HasMaxLength(128);
            Property(x => x.Label).HasMaxLength(128);
            HasRequired(x => x.PassContentTemplate).WithMany(x => x.PassContentTemplateFields).HasForeignKey(x => x.PassContentTemplateId);
            HasOptional(x => x.PassProjectField).WithMany().HasForeignKey(x => x.PassProjectFieldId);
        }
    }
}
