using Common.Repository;

namespace FileStorage.Core
{
    public interface IFileStorageUnitOfWork : IUnitOfWork
    {
        IFileStorageRepository FileStorageRepository { get; }
    }
}