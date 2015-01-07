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
        private IPassSiteUserRepository _passSiteUserRepository;
        private IPassSiteCertificateRepository _passSiteCertificateRepository;

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
                                            typeof(PassSiteCertificate),
                                            typeof(PassCertificateApple),
                                            typeof(PassSiteCertificate),
                                            typeof(PassProjectField)
                                        };

            RegisterCustomRepository(PassSiteRepository);
            RegisterCustomRepository(PassSiteUserRepository);
            RegisterCustomRepository(PassSiteCertificateRepository);
        }

        protected override HashSet<Type> AllowedRepositoryEntities
        {
            get
            {
                return _allowedRepositoryEntities;
            }
        }

        public IPassSiteRepository PassSiteRepository
        {
            get
            {
                return _passSiteRepository ?? (_passSiteRepository = new PassSiteRepository(_dbContext));
            }
        }
        public IPassSiteUserRepository PassSiteUserRepository
        {
            get
            {
                return _passSiteUserRepository ?? (_passSiteUserRepository = new PassSiteUserRepository(_dbContext));
            }
        }
        public IPassSiteCertificateRepository PassSiteCertificateRepository
        {
            get
            {
                return _passSiteCertificateRepository ?? (_passSiteCertificateRepository = new PassSiteCertificateRepository(_dbContext));
            }
        }
    }
}
