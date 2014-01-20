
using System;

namespace FileStorage.Core.Entities
{
    public abstract class BaseStorageItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public FolderItem Parent { get; set; }
        public ItemStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
