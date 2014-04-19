using System;
using System.Collections.Generic;
using Common.Repository;
using Common.Repository.EF;
using Pass.CertificateStorage.Repository.Core;
using Pass.CertificateStorage.Repository.Core.Entities;

namespace Pass.CertificateStorage.Repository.EF
{
    public class CertificateStorageUnitOfWork : UnitOfWork, ICertificateStorageUnitOfWork
    {
        private readonly HashSet<Type> _allowedRepositoryEntities;

        public CertificateStorageUnitOfWork(IDbConfig dbConfig)
            :base(new CertificateStorageDbContext(dbConfig.ConnectionString))
        {
            _allowedRepositoryEntities = new HashSet<Type>() { typeof(Certificate) };
        }

        protected override HashSet<Type> AllowedRepositoryEntities
        {
            get
            {
                return _allowedRepositoryEntities;
            }
        }

    }
}
