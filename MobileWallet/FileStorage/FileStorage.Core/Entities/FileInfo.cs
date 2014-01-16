namespace FileStorage.Core.Entities
{
    public class FileInfo : ItemInfo
    {
        public string OriginalName { get; set; }
        public int FileSize { get; set; }
    }
}
