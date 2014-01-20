namespace FileStorage.Core
{
    public interface IFileStorageConfig
    {
        int StorageDeep { get; }
        int ItemsNumber { get; }
        string StoragePath { get; }
    }
}