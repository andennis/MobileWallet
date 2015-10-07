using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassImageConfiguration: EntityTypeConfiguration<PassImage>
    {
        public PassImageConfiguration(string dbScheme)
        {
            ToTable("PassImage", dbScheme);
            Property(x => x.Version).IsConcurrencyToken();
            HasRequired(x => x.PassContentTemplate).WithMany(x => x.PassImages).HasForeignKey(x => x.PassContentTemplateId);
            Ignore(x => x.ImageFile);
            Ignore(x => x.ImageFile2x);
        }
    }
}
