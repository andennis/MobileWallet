using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;

namespace Pass.Notification.Repository.Core.Entities
{
    public class PushNotificationItem : EntityVersionable
    {
        public int PushNotificationItemId { get; set; }
        public int CertificateStorageId { get; set; }
        public string PushTockenId { get; set; }
        public int Status { get; set; }
    }
}
