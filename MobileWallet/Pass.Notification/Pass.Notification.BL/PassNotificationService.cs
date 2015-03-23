using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CertificateStorage.Core;
using CertificateStorage.Core.Entities;
using Common.Extensions;
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
        private readonly ICertificateStorageService _certificateStorageService;

        public PassNotificationService(IPushNotificationUnitOfWork pnUnitOfWork, IPushNotificationWorker pushNotificationWorker, ICertificateStorageService certificateStorageService)
        {
            _pnUnitOfWork = pnUnitOfWork;
            _pushNotificationWorker = pushNotificationWorker;
            _repPushNotificationItem = _pnUnitOfWork.GetRepository<PushNotificationItem>();
            _certificateStorageService = certificateStorageService;
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

        public void SendPushNotifications()
        {
            IQueryable<PushNotificationItem> allPushNotifications = _repPushNotificationItem.Query().Get();
            if (allPushNotifications == null)
                return;
        
            IEnumerable<IGrouping<int, PushNotificationItem>> pushNotificationsByCertificates = allPushNotifications.GroupBy(x => x.CertificateStorageId);
            foreach (IGrouping<int, PushNotificationItem> pushNotifications in pushNotificationsByCertificates)
            {
                X509Certificate2 certificate = GetCertificate(pushNotifications.Key);
                foreach (PushNotificationItem pushNotification in pushNotifications)
                {
                    _pushNotificationWorker.SendNotification(certificate, pushNotification.PushTockenId);
                }
            }
        }

        private X509Certificate2 GetCertificate(int certId)
        {
            using (CertificateInfo certInfo = _certificateStorageService.Read(certId))
            {
                var ms = new MemoryStream();
                certInfo.CertificateFile.CopyTo(ms);
                return new X509Certificate2(ms.ToArray(), certInfo.Password.ConvertToUnsecureString(),
                    X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
            }
        }

        #region IDisposable
        public void Dispose()
        {
            _pnUnitOfWork.Dispose();
        }
        #endregion
    }
}
