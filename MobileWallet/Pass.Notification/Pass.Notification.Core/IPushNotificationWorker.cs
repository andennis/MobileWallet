using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Pass.Notification.Core
{
    public interface IPushNotificationWorker
    {
        void SendNotification(X509Certificate2 certificate, IList<string> deviceTokenlList);
    }
}
