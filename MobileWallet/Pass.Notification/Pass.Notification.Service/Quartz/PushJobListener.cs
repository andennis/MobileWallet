using System;
using Atlas;
using Pass.Notification.BL.Utils;
using Quartz;

namespace Pass.Notification.Service.Quartz
{
    internal class PushJobListener : IJobListener
    {
        private readonly IContainerProvider _provider;
        private IUnitOfWorkContainer _container;
        
        public PushJobListener(IContainerProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            this._provider = provider;
            this.Name = "PushJobListener";
        }

        public string Name { get; private set; }

        public void JobToBeExecuted(IJobExecutionContext context)
        {
            this._container = this._provider.CreateUnitOfWork();
            this._container.InjectUnsetProperties(context.JobInstance);
        }
      
        public void JobExecutionVetoed(IJobExecutionContext context)
        {
        }
    
        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        { 
            Logger.Error(jobException.ToString());
            this._container.Dispose();
        }
    }
}
