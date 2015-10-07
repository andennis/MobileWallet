using System.Data.Entity.ModelConfiguration;
using Pass.Manager.Core.Entities;

namespace Pass.Manager.Repository.EF.Mapping
{
    public class PassBarcodeConfiguration : EntityTypeConfiguration<PassBarcode>
    {
        public PassBarcodeConfiguration(string dbScheme)
        {
            ToTable("PassBarcode", dbScheme);
            HasKey(x => x.PassContentTemplateId);
            Property(x => x.Version).IsConcurrencyToken();
            Property(x => x.Format).IsRequired();
            Property(x => x.Message).IsRequired().HasMaxLength(64);
            Property(x => x.MessageEncoding).HasMaxLength(32);
            HasRequired(x => x.PassContentTemplate).WithOptional(x => x.Barcode);
        }
    }
}
