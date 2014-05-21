﻿using System;
using System.Collections.Generic;
using Common.Repository;
using Common.Repository.EF;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.Repository.EF
{
    public class PassContainerUnitOfWork : UnitOfWork, IPassContainerUnitOfWork
    {
        private IPassRepository _passRepository;
        private readonly HashSet<Type> _allowedRepositoryEntities;

        public PassContainerUnitOfWork(IDbConfig dbConfig)
            :base(new PassContainerDbContext(dbConfig.ConnectionString))
        {
            _allowedRepositoryEntities = new HashSet<Type>()
                                             {
                                                 typeof(Core.Entities.Pass),
                                                 typeof(PassField),
                                                 typeof(PassFieldValue),
                                                 typeof(PassTemplate), 
                                                 typeof(PassTemplateNative),
                                                 typeof(PassTemplateApple), 
                                                 typeof(ClientDevice), 
                                                 typeof(ClientDeviceApple), 
                                                 typeof(Registration)
                                             };
        }

        protected override HashSet<Type> AllowedRepositoryEntities
        {
            get { return _allowedRepositoryEntities; }
        }

        public override IRepository<TEntity> GetRepository<TEntity>()
        {
            if (typeof(TEntity) == typeof(Core.Entities.Pass))
                return (IRepository<TEntity>)this.PassRepository;

            return base.GetRepository<TEntity>();
        }

        public IPassRepository PassRepository
        {
            get
            {
                return _passRepository ?? (_passRepository = new PassRepository(_dbContext));
            }
        }

    }
}
