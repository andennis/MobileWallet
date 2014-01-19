
namespace FileStorage.Core.Entities
{
    public class FileItem
    {
        public int FileItemId { get; set; }
        public string Name { get; set; }
        public FolderItem Parent { get; set; }
        public string OriginalName { get; set; }
        public int FileSize { get; set; }
    }
}
