using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Pass.Notification.BL;
using Pass.Notification.Core;
using Pass.Notification.Repository.Core.Enums;

namespace Pass.Notification.Service
{
    [ServiceContract]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PushNotificationService
    {
        private readonly IPassNotificationService _passNotificationService;

        public PushNotificationService(IPassNotificationService passNotificationService)
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
