using System;
using NUnit.Framework;
using Pass.Notification.Repository.Core;
using Pass.Notification.Repository.Core.Entities;

namespace Pass.Notification.Repository.EF.Tests
{
    [TestFixture]
    public class PushNotificationUnitOfWorkTests
    {
        private class UnknownEntity
        {
        }

        [Test]
        public void PushNotificationUnitOfWorkTest()
        {
            using (var unitOfWork = GetPushNotificationUnitOfWork())
            {
                Assert.NotNull(unitOfWork.PushNotificationRepository);
                Assert.IsInstanceOf<IPushNotificationRepository>(unitOfWork.PushNotificationRepository);
                Assert.IsInstanceOf<IPushNotificationRepository>(unitOfWork.GetRepository<PushNotificationItem>());
            }
        }

        [Test]
        public void GetRepositoryTest()
        {
            using (var unitOfWork = GetPushNotificationUnitOfWork())
            {
                Assert.IsNotNull(unitOfWork.GetRepository<PushNotificationItem>());
                Assert.Throws<Exception>(() => unitOfWork.GetRepository<UnknownEntity>());
            }
        }

        private IPushNotificationUnitOfWork GetPushNotificationUnitOfWork()
        {
            return new PushNotificationUnitOfWork(TestHelper.DbConfig);
        }
    }
}
