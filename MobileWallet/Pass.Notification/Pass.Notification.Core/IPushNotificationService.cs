using System;
using System.Collections.Generic;
using Pass.Notification.Core.Enums;

namespace Pass.Notification.Core
{
    public interface IPushNotificationService : IDisposable
    {
        void AddPushNotificationToQueue(int certificateStorageId, IEnumerable<string> pushTockens, PushNotificationServiceType serviceType);
        void SendPushNotificationFromQueue();
        void SendPushNotifications(int certificateId, IList<string> pushTockens);
    }
}
