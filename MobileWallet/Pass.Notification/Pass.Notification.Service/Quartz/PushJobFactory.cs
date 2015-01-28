using System;
using Autofac;
using Quartz;
using Quartz.Spi;

namespace Pass.Notification.Service.Quartz
{
    public class PushJobFactory : IJobFactory
    {
        private readonly IContainer _container;
      
        public PushJobFactory(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this._container = container;
        }
      
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            if (bundle == null)
                throw new ArgumentNullException("bundle");

            return (IJob)this._container.Resolve(bundle.JobDetail.JobType); 
        }
       
        public void ReturnJob(IJob job)
        {
        }
    }
}
