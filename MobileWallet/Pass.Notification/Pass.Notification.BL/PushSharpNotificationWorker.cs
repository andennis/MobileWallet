using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using Pass.Notification.Core;
using Pass.Notification.Repository.Core;
using Pass.Notification.Repository.Core.Entities;
using PushSharp;
using PushSharp.Android;
using PushSharp.Apple;
using PushSharp.Core;

namespace Pass.Notification.BL
{
    public class PushSharpNotificationWorker : IPushNotificationWorker
    {
        private readonly IPushNotificationUnitOfWork _pnUnitOfWork;
        private readonly IRepository<PushNotificationItem> _repPushNotificationItem;
        public PushSharpNotificationWorker(IPushNotificationUnitOfWork pnUnitOfWork)
        {
            _pnUnitOfWork = pnUnitOfWork;
            _repPushNotificationItem = pnUnitOfWork.GetRepository<PushNotificationItem>();
        }
        public void SendNotification(X509Certificate2 certificate, IList<string> deviceTokenlList)
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
            foreach (string deviceToken in deviceTokenlList)
            {
                push.QueueNotification(new AppleNotification().ForDeviceToken(deviceToken));
            }

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
