﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Common.Repository;
using Pass.Notification.Core;
using Pass.Notification.Repository.Core;
using Pass.Notification.Repository.Core.Entities;
using Pass.Notification.Repository.Core.Enums;
using PushSharp;
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

        // Notification has been sent successfully.
        private void NotificationSent(object sender, INotification notification)
        {
            var appleNotification = (AppleNotification)notification;
            var deviceTocken = appleNotification.DeviceToken;

            List<PushNotificationItem> failPushNotificationItems = _repPushNotificationItem.Query()
                .Filter(x => x.PushTockenId == deviceTocken && x.Status == PushStatus.InProcess)
                .Get().ToList();
            _pnUnitOfWork.PushNotificationRepository.SetPushNotificationStatus(failPushNotificationItems.Select(x => x.PushNotificationItemId).ToList(), PushStatus.Processed);
            _pnUnitOfWork.Save();
        }

         private void NotificationFailed(object sender, INotification notification, Exception notificationFailureException)
        {
            var appleNotification = (AppleNotification) notification;
            var deviceTocken = appleNotification.DeviceToken;

            List<PushNotificationItem> failPushNotificationItems = _repPushNotificationItem.Query()
                .Filter(x => x.PushTockenId == deviceTocken && x.Status == PushStatus.InProcess)
                .Get().ToList();
            _pnUnitOfWork.PushNotificationRepository.SetPushNotificationStatus(failPushNotificationItems.Select(x => x.PushNotificationItemId).ToList(), PushStatus.Error);
            _pnUnitOfWork.Save();
        }

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
