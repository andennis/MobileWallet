using System;
using System.Collections.Generic;
using Common.Repository;
using Common.Repository.EF;
using Pass.Notification.Repository.Core;
using Pass.Notification.Repository.Core.Entities;

namespace Pass.Notification.Repository.EF
{
    public sealed class PushNotificationUnitOfWork : UnitOfWork, IPushNotificationUnitOfWork
    {
        private IPushNotificationRepository _pushNotificationRepository;
        private readonly HashSet<Type> _allowedRepositoryEntities;

        public PushNotificationUnitOfWork(IDbConfig dbConfig)
            :base(new PushNotificationDbContext(dbConfig.ConnectionString))
        {
            _allowedRepositoryEntities = new HashSet<Type>() {typeof (PushNotificationItem)};

            RegisterCustomRepository(PushNotificationRepository);
        }

        protected override HashSet<Type> AllowedRepositoryEntities
        {
            get
            {
                return _allowedRepositoryEntities;
            }
        }

        public IPushNotificationRepository PushNotificationRepository
        {
            get
            {
                return _pushNotificationRepository ?? (_pushNotificationRepository = new PushNotificationRepository(_dbContext));
            }
        }
    }
}
