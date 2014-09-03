using Common.Repository;
using Common.Repository.EF;
using Pass.Manager.Core.Entities;
using Pass.Manager.Repository.Core;
using System;
using System.Collections.Generic;

namespace Pass.Manager.Repository.EF
{
    public class PassManagerUnitOfWork : UnitOfWork, IPassManagerUnitOfWork
    {
        private readonly HashSet<Type> _allowedRepositoryEntities;

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
    }
}
