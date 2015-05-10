using Pass.Notification.BL.Utils;
using Pass.Notification.Core;

namespace Pass.Notification.Service.Quartz
{
    public class PushLogicLayer: IPushLogicLayer
    {
        public void Run(IPushNotificationService passNotificationService)
        {
            passNotificationService.SendPushNotificationFromQueue();
            Logger.Info("Push has been run");
        }

        public void Dispose()
        {
        }
    }
}
