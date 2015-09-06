using System.Collections.Generic;

namespace Pass.Notification.Core
{
    public interface INotificationService
    { 
        void SendAppleNotifications(int certificateStorageId, IEnumerable<string> pushTockenIds);
    }
}
