using System.Configuration;
using Atlas;
using Pass.Notification.BL.Utils;
using Quartz;
using Quartz.Spi;

namespace Pass.Notification.Service.Quartz
{
    public class PushTaskService : IAmAHostedProcess
    {
        public IScheduler Scheduler { get; set; }    
        public IJobFactory JobFactory { get; set; }       
        public IJobListener JobListener { get; set; }      

        public void Start()
        {
            Logger.Info("PushTaskService starting");

            var job = JobBuilder.Create<PushJob>()
                                .WithIdentity("PushJob", "PushTaskService")
                                .Build();                   

            var trigger = TriggerBuilder.Create()
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
    }
}
