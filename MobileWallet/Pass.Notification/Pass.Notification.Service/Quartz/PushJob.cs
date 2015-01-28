using Pass.Notification.BL.Utils;
using Quartz;

namespace Pass.Notification.Service.Quartz
{
    public class PushJob : IJob
    {
        public IPushLogicLayer PushLogicLayer { get; set; }
       
        public void Execute(IJobExecutionContext context)
        {
            Logger.Info("Push notifications executing");

            this.PushLogicLayer.Run();

            Logger.Info("Push notifications executed");
        }
    }
}
