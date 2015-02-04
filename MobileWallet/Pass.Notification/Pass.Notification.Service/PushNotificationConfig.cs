using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Configuration;
using Pass.Notification.Core;

namespace Pass.Notification.Service
{
    public sealed class PushNotificationConfig: AppConfigBase, IPushNotificationConfig
    {
        public PushNotificationConfig()
            : base("PushNotification")
        {
        }

        public string ConnectionString
        {
            get { return GetValue("ConnectionStringName"); }
        }
    }
}
