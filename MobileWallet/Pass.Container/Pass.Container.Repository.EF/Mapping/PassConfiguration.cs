using System.Data.Entity.ModelConfiguration;

namespace Pass.Container.Repository.EF.Mapping
{
    public class PassConfiguration : EntityTypeConfiguration<Core.Entities.Pass>
    {
        public PassConfiguration(string dbScheme)
        {
            ToTable("Pass", dbScheme);
            HasRequired(x => x.Template).WithMany(x => x.Passes).HasForeignKey(x => x.PassTemplateId).WillCascadeOnDelete(false);
            Property(x => x.AuthToken).IsRequired().HasMaxLength(64).IsUnicode(false);
            Property(x => x.Version).IsConcurrencyToken();
        }
    }
}
