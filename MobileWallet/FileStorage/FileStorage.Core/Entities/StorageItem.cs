
using Common.Repository;

namespace FileStorage.Core.Entities
{
    public sealed class StorageItem : EntityVersionable
    {
        public int StorageItemId { get; set; }
        public string Name { get; set; }
        public FolderItem Parent { get; set; }
        public string OriginalName { get; set; }
        public long Size { get; set; }
        public ItemStatus Status { get; set; }
        public StorageItemType ItemType { get; set; }
    }
}
