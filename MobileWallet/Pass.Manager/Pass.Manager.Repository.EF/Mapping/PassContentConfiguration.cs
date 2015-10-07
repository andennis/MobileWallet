using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassContentConfiguration : EntityTypeConfiguration<PassContent>
    {
        public PassContentConfiguration(string dbScheme)
        {
            ToTable("PassContent", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            //Property(x => x.AuthToken).IsRequired().HasMaxLength(64).IsUnicode(false);
            //Property(x => x.SerialNumber).IsRequired().HasMaxLength(64).IsUnicode(false);
            HasRequired(x => x.PassContentTemplate).WithMany().HasForeignKey(x => x.PassContentTemplateId).WillCascadeOnDelete(false);
        }
    }
}
