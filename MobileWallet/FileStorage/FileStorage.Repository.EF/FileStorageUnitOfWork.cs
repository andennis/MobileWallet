using System;
using System.Collections.Generic;
using Common.Repository;
using Common.Repository.EF;
using FileStorage.Repository.Core;
using FileStorage.Repository.Core.Entities;

namespace FileStorage.Repository.EF
{
    public sealed class FileStorageUnitOfWork : UnitOfWork, IFileStorageUnitOfWork
    {
        private IFileStorageRepository _fileStorageRepository;
        private readonly HashSet<Type> _allowedRepositoryEntities;

        public FileStorageUnitOfWork(IDbConfig dbConfig)
            :base(new FileStorageDbContext(dbConfig.ConnectionString))
        {
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
                return _fileStorageRepository ?? (_fileStorageRepository = new FileStorageRepository(_dbContext));
            }
        }
    }
}
