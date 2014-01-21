
namespace FileStorage.Core.Entities
{
    public sealed class StorageItem
    {
        public int StorageItemId { get; set; }
        public string Name { get; set; }
        public FolderItem Parent { get; set; }
        public string OriginalName { get; set; }
        public int Size { get; set; }
        public ItemStatus Status { get; set; }
        public StorageItemType ItemType { get; set; }
    }
}
