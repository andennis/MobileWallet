using Pass.Notification.BL.Utils;
using Pass.Notification.Core;

namespace Pass.Notification.Service.Quartz
{
    public class PushLogicLayer: IPushLogicLayer
    {
        public void Run(IPassNotificationService passNotificationService)
        {
            passNotificationService.SendPushNotifications();
            Logger.Info("Push has been run");
        }

        public void Dispose()
        {
        }
    }
}
