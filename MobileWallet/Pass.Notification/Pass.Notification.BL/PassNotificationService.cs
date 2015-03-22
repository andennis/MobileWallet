using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using Pass.Notification.Core;
using Pass.Notification.Repository.Core;
using Pass.Notification.Repository.Core.Entities;
using Pass.Notification.Repository.Core.Enums;

namespace Pass.Notification.BL
{
    public class PassNotificationService : IPassNotificationService
    {
        private readonly IPushNotificationUnitOfWork _pnUnitOfWork;
        private readonly IPushNotificationWorker _pushNotificationWorker;
        private readonly IRepository<PushNotificationItem> _repPushNotificationItem;

        public PassNotificationService(IPushNotificationUnitOfWork pnUnitOfWork, IPushNotificationWorker pushNotificationWorker)
        {
            _pnUnitOfWork = pnUnitOfWork;
            _pushNotificationWorker = pushNotificationWorker;
            _repPushNotificationItem = _pnUnitOfWork.GetRepository<PushNotificationItem>();
        }

        public void AddPushNotificationToQueue(int certificateStorageId, IEnumerable<string> pushTockenIds, PushNotificationServiceType serviceType)
        {
            foreach (string pushTockenId in pushTockenIds)
            {
                var pushNotification = new PushNotificationItem
                                       {
                                           CertificateStorageId = certificateStorageId,
                                           PushTockenId = pushTockenId,
                                           Status = PushStatus.Pending,
                                           PushNotificationServiceType = serviceType
                                       };
                _repPushNotificationItem.Insert(pushNotification);
            }

            _pnUnitOfWork.Save();
        }

        #region IDisposable
        public void Dispose()
        {
            _pnUnitOfWork.Dispose();
        }
        #endregion
    }
}
