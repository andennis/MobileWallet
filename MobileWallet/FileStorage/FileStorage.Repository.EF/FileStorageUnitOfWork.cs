using System.Data.Entity;
using Common.Repository;
using Common.Repository.EF;
using FileStorage.Core;
using FileStorage.Core.Entities;

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
            AddRepository(new Repository<StorageItem>((DbContext)dbSession.DbContext));
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
