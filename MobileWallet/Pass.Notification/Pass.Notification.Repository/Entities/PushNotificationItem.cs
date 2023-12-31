﻿using Common.Repository;
using Pass.Notification.Core.Enums;
using Pass.Notification.Repository.Core.Enums;

namespace Pass.Notification.Repository.Core.Entities
{
    public class PushNotificationItem : EntityVersionable
    {
        public int PushNotificationItemId { get; set; }
        public int CertificateStorageId { get; set; }
        public string PushTockenId { get; set; }
        public PushStatus Status { get; set; }
        public PushNotificationServiceType PushNotificationServiceType { get; set; }
        
    }
}
