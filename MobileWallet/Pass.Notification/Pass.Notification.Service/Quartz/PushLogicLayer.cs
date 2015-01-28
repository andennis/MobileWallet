using Pass.Notification.BL.Utils;

namespace Pass.Notification.Service.Quartz
{
    public class PushLogicLayer: IPushLogicLayer
    {
        public void Run()
        {
            Logger.Info("This has been run");
        }

        public void Dispose()
        {
        }
    }
}
