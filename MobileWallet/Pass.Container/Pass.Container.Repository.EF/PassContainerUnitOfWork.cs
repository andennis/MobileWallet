using System;
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
        private ISequenceCounterRepository _sequenceCounterRepository;
        private readonly HashSet<Type> _allowedRepositoryEntities;

        public PassContainerUnitOfWork(IDbConfig dbConfig)
            :base(new PassContainerDbContext(dbConfig.ConnectionString))
        {
            _allowedRepositoryEntities = new HashSet<Type>()
                                             {
                                                 typeof(Core.Entities.Pass),
                                                 typeof(PassNative),
                                                 typeof(PassApple),
                                                 typeof(PassField),
                                                 typeof(PassFieldValue),
                                                 typeof(PassTemplate), 
                                                 typeof(PassTemplateNative),
                                                 typeof(PassTemplateApple), 
                                                 typeof(ClientDevice), 
                                                 typeof(ClientDeviceApple), 
                                                 typeof(Registration),
                                                 typeof(SequenceCounter)
                                             };

            RegisterCustomRepository(PassRepository);
            RegisterCustomRepository(SequenceCounterRepository);
        }

        protected override HashSet<Type> AllowedRepositoryEntities
        {
            get { return _allowedRepositoryEntities; }
        }

        public IPassRepository PassRepository
        {
            get
            {
                return _passRepository ?? (_passRepository = new PassRepository(_dbContext));
            }
        }

        public ISequenceCounterRepository SequenceCounterRepository
        {
            get
            {
                return _sequenceCounterRepository ?? (_sequenceCounterRepository = new SequenceCounterRepository(_dbContext));
            }
        }

    }
}
