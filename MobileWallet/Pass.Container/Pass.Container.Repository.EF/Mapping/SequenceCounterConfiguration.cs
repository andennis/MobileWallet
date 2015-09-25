using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF.Mapping
{
    public class SequenceCounterConfiguration : EntityTypeConfiguration<SequenceCounter>
    {
        public SequenceCounterConfiguration(string dbScheme)
        {
            ToTable("SequenceCounter", dbScheme);
            Property(x => x.Name).HasMaxLength(50)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("IX_SequenceCounter_Name") {IsUnique = true}));
        }
    }
}
