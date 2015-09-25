using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    class PassFieldValueConfiguration : EntityTypeConfiguration<PassFieldValue>
    {
        public PassFieldValueConfiguration(string dbScheme)
        {
            ToTable("PassFieldValue", dbScheme);
            HasRequired(x => x.Pass).WithMany(x => x.FieldValues).HasForeignKey(x => x.PassId);
            HasRequired(x => x.PassField).WithMany(x => x.FieldValues).HasForeignKey(x => x.PassFieldId).WillCascadeOnDelete(false);
            Property(x => x.Label).HasMaxLength(512);
            Property(x => x.Value).HasMaxLength(512);
            Property(x => x.Version).IsConcurrencyToken();
        }
    }
}
