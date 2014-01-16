using System.Collections.Generic;

namespace FileStorage.Core.Entities
{
    public class ItemInfo
    {
        public int ItemInfoId { get; set; }
        public string Name { get; set; }
        public ItemInfo ParentItem { get; set; }
        public ICollection<ItemInfo> ChildItems { get; set; }
    }
}
