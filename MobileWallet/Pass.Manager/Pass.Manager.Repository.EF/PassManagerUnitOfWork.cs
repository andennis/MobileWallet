using Common.Repository;
using Common.Repository.EF;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using System;
using System.Collections.Generic;
using Pass.Manager.Core.Repositories;

namespace Pass.Manager.Repository.EF
{
    public class PassManagerUnitOfWork : UnitOfWork, IPassManagerUnitOfWork
    {
        private readonly HashSet<Type> _allowedRepositoryEntities;
        private IPassSiteRepository _passSiteRepository;
        public PassManagerUnitOfWork(IDbConfig dbConfig)
            : base(new PassManagerDbContext(dbConfig.ConnectionString))
        {
            _allowedRepositoryEntities = new HashSet<Type>() 
                                        { 
                                            typeof(PassSite),
                                            typeof(PassProject),
                                            typeof(User),
                                            typeof(PassSiteUser),
                                            typeof(PassCertificate),
                                            typeof(PassCertificateApple),
                                            typeof(PassSiteCertificate)
                                        };
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
            if (typeof(TEntity) == typeof(PassSite))
                return (IRepository<TEntity>)this.PassSiteRepository;

            return base.GetRepository<TEntity>();
        }

        public IPassSiteRepository PassSiteRepository
        {
            get
            {
                return _passSiteRepository ?? (_passSiteRepository = new PassSiteRepository(_dbContext));
            }
        }

    }
}
