using System.Data.Entity.ModelConfiguration;
using FileStorage.Repository.Core.Entities;

namespace FileStorage.Repository.EF.Mapping
{
    public class FolderItemConfiguration : EntityTypeConfiguration<FolderItem>
    {
        public FolderItemConfiguration(string dbScheme)
        {
            ToTable("FolderItem", dbScheme);
            HasOptional(x => x.Parent).WithMany(x => x.ChildFolders).Map(x => x.MapKey("ParentId"));
            Property(x => x.Name).IsRequired().HasMaxLength(512);
        }
    }
}
