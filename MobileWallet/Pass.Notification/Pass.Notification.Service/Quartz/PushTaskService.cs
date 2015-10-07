using System;
using System.Configuration;
using Atlas;
using Pass.Notification.BL.Utils;
using Pass.Notification.Core;
using Quartz;
using Quartz.Spi;

namespace Pass.Notification.Service.Quartz
{
    public class PushTaskService : IAmAHostedProcess, IDisposable
    {
        private readonly IPushNotificationService _passNotificationService;
        public IScheduler Scheduler { get; set; }    
        public IJobFactory JobFactory { get; set; }       
        public IJobListener JobListener { get; set; }

        public PushTaskService(IPushNotificationService passNotificationService)
        {
            _passNotificationService = passNotificationService;
        }

        public void Start()
        {
            Logger.Info("PushTaskService starting");
            bool isDebugMode = false;
            Boolean.TryParse(ConfigurationManager.AppSettings["IsDebugMode"], out isDebugMode);
            if (isDebugMode)
                Logger.Info("PushTaskService starting in Debug Mode");

            PushNotificationServiceHost.StartPushNotificationServiceHosts(_passNotificationService);

            Logger.Info("Push Notification sheduler starting");
            IJobDetail job = JobBuilder.Create<PushJob>()
                                .WithIdentity("PushJob", "PushTaskService")
                                .Build();
            job.JobDataMap.Add("PassNotificationService", _passNotificationService);
            job.JobDataMap.Add("IsDebugMode", isDebugMode); 

            ITrigger trigger = TriggerBuilder.Create()
                                        .WithIdentity("PushTrigger", "PushTaskService")
                                        .WithCronSchedule(ConfigurationManager.AppSettings["CronExpression"])
                                        .ForJob("PushJob", "PushTaskService")
                                        .Build();         

            this.Scheduler.JobFactory = this.JobFactory;   
            this.Scheduler.ScheduleJob(job, trigger);      
            this.Scheduler.ListenerManager.AddJobListener(this.JobListener);    
            this.Scheduler.Start();

            Logger.Info("PushTaskService started");
        }

        public void Stop()
        {
            Logger.Info("PushTaskService stopping");
            PushNotificationServiceHost.StopPushNotificationServiceHosts();

            Logger.Info("Push Notification sheduler stopping");
            this.Scheduler.Shutdown();

            Logger.Info("PushTaskService stopped");
        }

      
        public void Resume()
        {
            Logger.Info("PushTaskService resuming");
            this.Scheduler.ResumeAll();
            Logger.Info("PushTaskService resumed");
        }

        
        public void Pause()
        {
            Logger.Info("PushTaskService pausing");
            this.Scheduler.PauseAll();
            Logger.Info("PushTaskService paused");
        }

        public void Dispose()
        {
            PushNotificationServiceHost.StopPushNotificationServiceHosts();
        }
    }
}
