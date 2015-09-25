using System.Data.Entity.ModelConfiguration;
using FileStorage.Repository.Core.Entities;

namespace FileStorage.Repository.EF.Mapping
{
    public class StorageItemConfiguration : EntityTypeConfiguration<StorageItem>
    {
        public StorageItemConfiguration(string dbScheme)
        {
            ToTable("StorageItem", dbScheme);
            HasRequired(x => x.Parent).WithMany(x => x.ChildStorageItems).Map(x => x.MapKey("ParentId"));
            Property(x => x.Name).IsRequired().HasMaxLength(512);
            Property(x => x.OriginalName).IsOptional().HasMaxLength(512);
            Property(x => x.Size).IsOptional();
        }
    }
}
