using Common.Repository;
using Common.Repository.EF;
using FileStorage.Core;

namespace FileStorage.Repository.EF
{
    public class FileStorageUnitOfWork : UnitOfWork, IFileStorageUnitOfWork
    {
        private readonly IDbSession _dbSession;
        private IFileStorageRepository _fileStorageRepository;

        public FileStorageUnitOfWork(IDbSession dbSession)
            :base(dbSession)
        {
            _dbSession = dbSession;
        }

        public IFileStorageRepository FileStorageRepository
        {
            get
            {
                return _fileStorageRepository ?? (_fileStorageRepository = new FileStorageRepository(_dbSession));
            }
        }
    }
}
