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

            bool isDebugMode = (bool)dataMap.Get("IsDebugMode");
            if (isDebugMode)
            {
                Logger.Info("DEBUG MODE: To send notifications enter '1' or '0' to skip");
                bool done = false;
                while (!done)
                {
                    Console.Write("DEBUG MODE: ");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Logger.Info("DEBUG MODE: Sending notifications.");
                            done = true;
                            break;
                        case "0":
                            Logger.Info("DEBUG MODE: Skip of sending notifications.");
                            return;
                        default:
                            Logger.Info("DEBUG MODE: You can enter '1' or '0'. To send notifications enter '1' or '0' to skip");
                            break;
                    }
                }
            }
             
            IPushNotificationService passNotificationService = (IPushNotificationService)dataMap.Get("PassNotificationService");
            if (passNotificationService == null)
                throw new NullReferenceException("PassNotificationService should not be null.");
            else
                this.PushLogicLayer.Run(passNotificationService);

            Logger.Info("Push notifications executed");
        }
    }
}
