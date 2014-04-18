using System;
using System.Collections.Generic;
using Common.Repository;
using Common.Repository.EF;
using Pass.CertificateStorage.Repository.Core;
using Pass.CertificateStorage.Repository.Core.Entities;

namespace Pass.CertificateStorage.Repository.EF
{
    public class PassCertificateStorageUnitOfWork : UnitOfWork, IPassCertificateStorageUnitOfWork
    {
        private readonly HashSet<Type> _allowedRepositoryEntities;

        public PassCertificateStorageUnitOfWork(IDbConfig dbConfig)
            :base(new PassCertificateStorageDbContext(dbConfig.ConnectionString))
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
