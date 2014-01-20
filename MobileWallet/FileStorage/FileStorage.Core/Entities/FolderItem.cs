using System.Collections.Generic;

namespace FileStorage.Core.Entities
{
    public class FolderItem : BaseStorageItem
    {
        //public int FolderItemId { get; set; }
        //public string Name { get; set; }
        //public FolderItem Parent { get; set; }
        public bool IsStorageItem { get; set; }
        public ICollection<FolderItem> ChildFolders { get; set; }
        public ICollection<FileItem> ChildFiles { get; set; }
        //public ItemStatus Status { get; set; }
    }
}
