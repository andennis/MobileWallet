using System;
using System.Collections.Generic;
using Common.Repository;
using Common.Repository.EF;
using FileStorage.Core;
using FileStorage.Core.Entities;

namespace FileStorage.Repository.EF
{
    public sealed class FileStorageUnitOfWork : UnitOfWork, IFileStorageUnitOfWork
    {
        private readonly IDbSession _dbSession;
        private IFileStorageRepository _fileStorageRepository;
        private readonly HashSet<Type> _allowedRepositoryEntities;

        public FileStorageUnitOfWork(IDbSession dbSession)
            :base(dbSession)
        {
            _dbSession = dbSession;
            _allowedRepositoryEntities = new HashSet<Type>() {typeof (FolderItem), typeof(StorageItem)};
        }

        protected override HashSet<Type> AllowedRepositoryEntities
        {
            get
            {
                return _allowedRepositoryEntities;
            }
        }

        public override IRepository<TEntity> GetRepository<TEntity>()
        {
            if (typeof(TEntity) == typeof(FolderItem))
                return (IRepository<TEntity>)this.FileStorageRepository;

            return base.GetRepository<TEntity>();
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
