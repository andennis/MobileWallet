using System.Collections.Generic;
using System.ServiceModel;
using Pass.Notification.Core;
using Pass.Notification.Core.Enums;

namespace Pass.Notification.Service
{
    [ServiceContract]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class NotificationService : INotificationService
    {
        private readonly IPushNotificationService _passNotificationService;

        public NotificationService(IPushNotificationService passNotificationService)
        {
            _passNotificationService = passNotificationService;
        }

        [OperationContract]
        public void SendAppleNotifications(int certificateStorageId, IEnumerable<string> pushTockenIds)
        {
            _passNotificationService.AddPushNotificationToQueue(certificateStorageId, pushTockenIds, PushNotificationServiceType.Apple);
        }
    }
}
