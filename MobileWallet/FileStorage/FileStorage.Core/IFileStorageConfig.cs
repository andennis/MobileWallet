using Common.Repository;

namespace FileStorage.Core
{
    public interface IFileStorageConfig : IDbConfig
    {
        int StorageDeep { get; }
        int MaxItemsNumber { get; }
        string StoragePath { get; }
    }
}