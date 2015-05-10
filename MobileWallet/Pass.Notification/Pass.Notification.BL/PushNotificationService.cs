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
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IPushNotificationUnitOfWork _pnUnitOfWork;
        private readonly IPushNotificationWorker _pushNotificationWorker;
        private readonly ICertificateStorageService _certificateStorageService;

        public PushNotificationService(IPushNotificationUnitOfWork pnUnitOfWork, IPushNotificationWorker pushNotificationWorker, ICertificateStorageService certificateStorageService)
        {
            _pnUnitOfWork = pnUnitOfWork;
            _pushNotificationWorker = pushNotificationWorker;
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
                _pnUnitOfWork.PushNotificationRepository.Insert(pushNotification);
            }

            _pnUnitOfWork.Save();
        }

        public void SendPushNotificationFromQueue()
        {
            List<PushNotificationItem> allPushNotifications = _pnUnitOfWork.PushNotificationRepository.Query().Filter(x => x.Status == PushStatus.Pending).Get().ToList();
            if (allPushNotifications == null)
                return;

            _pnUnitOfWork.PushNotificationRepository.SetPushNotificationStatus(allPushNotifications.Select(x => x.PushNotificationItemId).ToList(), PushStatus.InProcess);
            IEnumerable<IGrouping<int, PushNotificationItem>> pushNotificationsByCertificates = allPushNotifications.GroupBy(x => x.CertificateStorageId);
           
            foreach (IGrouping<int, PushNotificationItem> pushNotifications in pushNotificationsByCertificates)
                this.SendPushNotifications(pushNotifications.Key, pushNotifications.Select(x => x.PushTockenId).ToList());
        }

        public void SendPushNotifications(int certificateId, IList<string> deviceTokenlList)
        {
            X509Certificate2 certificate = GetCertificate(certificateId);
            _pushNotificationWorker.SendNotification(certificate, deviceTokenlList);
        }

        private X509Certificate2 GetCertificate(int certId)
        {
            using (CertificateInfo certInfo = _certificateStorageService.Read(certId))
            {
                var ms = new MemoryStream();
                certInfo.CertificateFile.ContentStream.CopyTo(ms);
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
