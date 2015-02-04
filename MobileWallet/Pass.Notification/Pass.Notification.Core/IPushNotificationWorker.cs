using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Notification.Core
{
    public interface IPushNotificationWorker
    {
        void SendNotification();
    }
}
