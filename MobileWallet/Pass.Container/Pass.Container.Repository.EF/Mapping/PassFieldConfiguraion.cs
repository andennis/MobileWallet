using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    class PassFieldConfiguraion : EntityTypeConfiguration<PassField>
    {
        public PassFieldConfiguraion(string dbScheme)
        {
            ToTable("PassField", dbScheme);
            HasRequired(x => x.Template).WithMany(x => x.PassFields).HasForeignKey(x => x.PassTemplateId);
            Property(x => x.Name).IsRequired().HasMaxLength(DbFieldSettings.FieldLenName);
            Property(x => x.Version).IsConcurrencyToken();
        }
    }
}
