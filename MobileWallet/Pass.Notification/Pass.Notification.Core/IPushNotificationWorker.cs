using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Notification.Core
{
    public interface IPushNotificationWorker
    {
        void SendNotification(X509Certificate2 certificate, string deviceToken);
    }
}
