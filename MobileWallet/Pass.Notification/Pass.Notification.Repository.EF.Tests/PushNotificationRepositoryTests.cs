using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Pass.Notification.Repository.Core;
using Pass.Notification.Repository.Core.Entities;

namespace Pass.Notification.Repository.EF.Tests
{
    [TestFixture]
    public class PushNotificationRepositoryTests
    {
        [SetUp]
        public void InitEachTest()
        {
            using (var unitOfWork = GetPushNotificationUnitOfWork())
            {
                //unitOfWork.PushNotificationRepository.ClearFileStorage();
            }
        }

        [Test]
        public void PushNotificationCrudOperationsTest()
        {
            using (var unitOfWork = GetPushNotificationUnitOfWork())
            {
                var fiRep = unitOfWork.GetRepository<PushNotificationItem>();
                Assert.NotNull(fiRep);

                var fi1 = new PushNotificationItem() { CertificateStorageId = 1, PushTockenId = "1234567890", Status = 1};
                Assert.DoesNotThrow(() => fiRep.Insert(fi1));
                Assert.DoesNotThrow(unitOfWork.Save);
                Assert.Greater(fi1.PushNotificationItemId, 0);

                PushNotificationItem copyFi1 = null;
                Assert.DoesNotThrow(() => copyFi1 = fiRep.Find(fi1.PushNotificationItemId));
                Assert.AreEqual(fi1.CertificateStorageId, copyFi1.CertificateStorageId);
                Assert.AreEqual(fi1.PushTockenId, copyFi1.PushTockenId);

                fi1.CertificateStorageId = 2;
                Assert.DoesNotThrow(() => fiRep.Update(fi1));
                Assert.DoesNotThrow(unitOfWork.Save);
                copyFi1 = fiRep.Find(fi1.PushNotificationItemId);
                Assert.NotNull(copyFi1);
                Assert.AreEqual(fi1.CertificateStorageId, copyFi1.CertificateStorageId);

                Assert.DoesNotThrow(() => fiRep.Delete(fi1));
                Assert.DoesNotThrow(unitOfWork.Save);
                copyFi1 = fiRep.Find(fi1.PushNotificationItemId);
                Assert.Null(copyFi1);

                Assert.DoesNotThrow(() => fiRep.Delete(fi1));
                Assert.Catch<Exception>(unitOfWork.Save);
                copyFi1 = fiRep.Find(fi1.PushNotificationItemId);
                Assert.NotNull(copyFi1);
                copyFi1 = fiRep.Query().Filter(x => x.PushNotificationItemId == fi1.PushNotificationItemId).Get().FirstOrDefault();
                Assert.Null(copyFi1);
            }
        }

        private IPushNotificationUnitOfWork GetPushNotificationUnitOfWork()
        {
            return new PushNotificationUnitOfWork(TestHelper.DbConfig);
        }

    }
}
