using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassContentFieldConfiguration : EntityTypeConfiguration<PassContentField>
    {
        public PassContentFieldConfiguration(string dbScheme)
        {
            ToTable("PassContentField", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            HasRequired(x => x.PassProjectField).WithMany().HasForeignKey(x => x.PassProjectFieldId).WillCascadeOnDelete(false);
            Property(x => x.FieldLabel).HasMaxLength(DbFieldSettings.FieldLenName);
            Property(x => x.FieldValue).HasMaxLength(512);
            HasRequired(x => x.PassContent).WithMany(x => x.Fields).HasForeignKey(x => x.PassContentId);
        }
    }
}