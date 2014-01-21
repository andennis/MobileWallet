using System.Collections.Generic;

namespace FileStorage.Core.Entities
{
    public sealed class FolderItem
    {
        public int FolderItemId { get; set; }
        public string Name { get; set; }
        public FolderItem Parent { get; set; }
        public ICollection<FolderItem> ChildFolders { get; set; }
        public ICollection<StorageItem> ChildStorageItems { get; set; }
    }
}
