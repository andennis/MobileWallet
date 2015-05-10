using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Notification.Repository.Core.Enums;

namespace Pass.Notification.Core
{
    public interface IPushNotificationService : IDisposable
    {
        void AddPushNotificationToQueue(int certificateStorageId, IEnumerable<string> pushTockenIds, PushNotificationServiceType serviceType);
        void SendPushNotificationFromQueue();
        void SendPushNotifications(int certificateId, IList<string> deviceTokenlList);
    }
}
