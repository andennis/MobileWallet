using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Notification.Repository.Core.Enums;

namespace Pass.Notification.Core
{
    public interface IPassNotificationService : IDisposable
    {
        void AddPushNotificationToQueue(int certificateStorageId, IEnumerable<string> pushTockenIds, PushNotificationServiceType serviceType);
        void SendPushNotifications();
    }
}
