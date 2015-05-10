using System.Reflection;
using Atlas;
using Autofac;
using CertificateStorage.BL;
using CertificateStorage.Core;
using CertificateStorage.Repository.EF;
using FileStorage.BL;
using FileStorage.Core;
using FileStorage.Factory;
using FileStorage.Repository.EF;
using Pass.Notification.BL;
using Pass.Notification.Core;
using Pass.Notification.Repository.EF;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Module = Autofac.Module;

namespace Pass.Notification.Service.Quartz
{
    public class PushTaskModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.LoadQuartz(builder);
            this.LoadServices(builder);
            this.LoadLogicLayers(builder);
        }
       
        private void LoadQuartz(ContainerBuilder builder)
        {
            builder.Register(c => new StdSchedulerFactory().GetScheduler())
                   .As<IScheduler>()
                   .InstancePerLifetimeScope(); 
            builder.Register(c => new PushJobFactory(ContainerProvider.Instance.ApplicationContainer))
                   .As<IJobFactory>();         
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(p => typeof(IJob).IsAssignableFrom(p))
                   .PropertiesAutowired();     
            builder.Register(c => new PushJobListener(ContainerProvider.Instance))
                   .As<IJobListener>();         
        }
      
        private void LoadServices(ContainerBuilder builder)
        {
            ICertificateStorageConfig certificateStorageConfig = new CertificateStorageConfig();
            IFileStorageConfig fileStorageConfig = new FileStorageConfig();
            builder.Register(c => new PushNotificationService(
                new PushNotificationUnitOfWork(new PushNotificationConfig()),
                new PushSharpNotificationWorker(new PushNotificationUnitOfWork(new PushNotificationConfig())),
                new CertificateStorageService(certificateStorageConfig,
                    new CertificateStorageUnitOfWork(certificateStorageConfig),
                    FileStorageFactory.Create(fileStorageConfig))))
                .As<IPushNotificationService>();
            builder.RegisterType<PushTaskService>()
                   .As<IAmAHostedProcess>()
                   .PropertiesAutowired();     
        }
      
        private void LoadLogicLayers(ContainerBuilder builder)
        {
            builder.RegisterType<PushLogicLayer>()
                   .As<IPushLogicLayer>();   
        }
    }
}
