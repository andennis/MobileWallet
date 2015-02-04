using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PushSharp;
using PushSharp.Android;
using PushSharp.Apple;
using PushSharp.Core;

namespace Pass.Notification.BL
{
    public class PushSharpNotificationWorker
    {
        public void SendNotification(X509Certificate2 certificate, string deviceToken)
        {
            //Create our push services broker
            var push = new PushBroker();

            //Wire up the events for all the services that the broker registers
            push.OnNotificationSent += NotificationSent;
            push.OnChannelException += ChannelException;
            push.OnServiceException += ServiceException;
            push.OnNotificationFailed += NotificationFailed;
            push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            push.OnChannelCreated += ChannelCreated;
            push.OnChannelDestroyed += ChannelDestroyed;

            //-------------------------
            // APPLE NOTIFICATIONS
            //-------------------------
            push.RegisterAppleService(new ApplePushChannelSettings(true, certificate, true));
            push.QueueNotification(new AppleNotification().ForDeviceToken(deviceToken));

            //"Waiting for Queue to Finish..."

            //Stop and wait for the queues to drains
            push.StopAllServices();
        }

        static void NotificationSent(object sender, INotification notification)
        {
            
        }

        static void NotificationFailed(object sender, INotification notification, Exception notificationFailureException)
        {}

        static void ChannelException(object sender, IPushChannel channel, Exception exception)
        {}

        static void ServiceException(object sender, Exception exception)
        {}

        static void DeviceSubscriptionExpired(object sender, string expiredDeviceSubscriptionId, DateTime timestamp, INotification notification)
        {}

        static void ChannelDestroyed(object sender)
        {}

        static void ChannelCreated(object sender, IPushChannel pushChannel)
        {}
    }
}
