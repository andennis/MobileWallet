using Common.Configuration;
using Pass.Notification.Core;

namespace Pass.Notification.BL
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
