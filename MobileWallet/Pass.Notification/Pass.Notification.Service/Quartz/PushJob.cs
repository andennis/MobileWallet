using System;
using Pass.Notification.BL.Utils;
using Pass.Notification.Core;
using Quartz;

namespace Pass.Notification.Service.Quartz
{
    public class PushJob : IJob
    {
        public IPushLogicLayer PushLogicLayer { get; set; }

        //public PushJob(IPassNotificationService passNotificationService)
        //{
            
        //}
       
        public void Execute(IJobExecutionContext context)
        {
            Logger.Info("Push notifications executing");
            JobDataMap dataMap = context.JobDetail.JobDataMap;


            IPushNotificationService passNotificationService = (IPushNotificationService)dataMap.Get("PassNotificationService");
            if (passNotificationService == null)
                throw new NullReferenceException("PassNotificationService should not be null.");
            else
                this.PushLogicLayer.Run(passNotificationService);

            Logger.Info("Push notifications executed");
        }
    }
}
