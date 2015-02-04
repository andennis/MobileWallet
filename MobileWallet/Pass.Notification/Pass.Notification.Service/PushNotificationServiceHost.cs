using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Pass.Notification.BL.Utils;
using Pass.Notification.Core;

namespace Pass.Notification.Service
{
    public static class PushNotificationServiceHost
    {
        private static ServiceHost _pushNotificationServiceHost;

        public static void StartPushNotificationServiceHosts(IPassNotificationService passNotificationService)
        {
            StopPushNotificationServiceHosts();

            _pushNotificationServiceHost = new ServiceHost(new PushNotificationService(passNotificationService));
            _pushNotificationServiceHost.Open();
            Logger.Info("Push notification service (WCF) has been started");
        }
        public static void StopPushNotificationServiceHosts()
        {
            if (_pushNotificationServiceHost != null)
            {
                _pushNotificationServiceHost.Close();
                _pushNotificationServiceHost = null;
                Logger.Info("Push notification service (WCF) has been stopped");
            }
        }
    }
}
