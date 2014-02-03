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
        //private readonly IDbSession _dbSession;
        private readonly HashSet<Type> _allowedRepositoryEntities;

        public PassContainerUnitOfWork(IDbSession dbSession)
            :base(dbSession)
        {
            //_dbSession = dbSession;
            _allowedRepositoryEntities = new HashSet<Type>()
                                             {
                                                 typeof(PassEntities.Pass),
                                                 typeof(PassEntities.PassTemplate), 
                                                 typeof(PassEntities.PassTemplateApple), 
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
