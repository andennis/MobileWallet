using System;
using System.Collections.Generic;
using Common.Repository;
using Common.Repository.EF;
using Pass.Container.Core;
using PassEntities = Pass.Container.Core.Entities;

namespace Pass.Container.Repository.EF
{
    public class PassContainerUnitOfWork : UnitOfWork, IPassContainerUnitOfWork
    {
        private readonly HashSet<Type> _allowedRepositoryEntities;

        public PassContainerUnitOfWork(IDbConfig dbConfig)
            :base(new PassContainerDbContext(dbConfig.ConnectionString))
        {
            _allowedRepositoryEntities = new HashSet<Type>()
                                             {
                                                 typeof(PassEntities.Pass),
                                                 typeof(PassEntities.PassField),
                                                 typeof(PassEntities.PassFieldValue),
                                                 typeof(PassEntities.PassTemplate), 
                                                 typeof(PassEntities.PassTemplateNative),
                                                 typeof(PassEntities.PassTemplateApple), 
                                                 typeof(PassEntities.ClientDevice), 
                                                 typeof(PassEntities.ClientDeviceApple), 
                                                 typeof(PassEntities.Registration)
                                             };
        }

        protected override HashSet<Type> AllowedRepositoryEntities
        {
            get { return _allowedRepositoryEntities; }
        }
    }
}
