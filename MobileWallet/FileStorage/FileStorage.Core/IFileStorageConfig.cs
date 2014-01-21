namespace FileStorage.Core
{
    public interface IFileStorageConfig
    {
        int StorageDeep { get; }
        int MaxItemsNumber { get; }
        string StoragePath { get; }
    }
}