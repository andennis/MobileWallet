using System.Collections.Generic;
using Common.WebService;
using Pass.Notification.Core;

namespace Pass.Notification.Service.Client
{
    public class PassNotificationServiceClient : ServiceChannelClient<INotificationService>, INotificationService 
    {
        public void SendAppleNotifications(int certificateStorageId, IEnumerable<string> pushTockenIds)
        {
            InvokeMethod(c => c.SendAppleNotifications(certificateStorageId, pushTockenIds));
        }
    }
}
