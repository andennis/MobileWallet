using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas;
using Autofac;
using Common.Repository;
using Common.Repository.EF;
using log4net;
using Pass.Notification.BL;
using Pass.Notification.BL.Utils;
using Pass.Notification.Core;
using Pass.Notification.Repository.Core;
using Pass.Notification.Repository.EF;
using Pass.Notification.Service.Quartz;

namespace Pass.Notification.Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var configuration = Host.UseAppConfig<PushTaskService>()
                    .AllowMultipleInstances()
                    .WithRegistrations(p => p.RegisterModule(new PushTaskModule()));
                    /*ADDITIONAL FUNCTIONALITY
                        // something to do before the service is actually started
                    .BeforeStart(Init)
                    // can fluently name the service, display names and descriptions are optional
                    .Named("TheServiceName", "Friendly Display Name", "My Service written by My Company or something")
                    // can optionally pass the command line arguments into Atlas.  If no arguments are passed in, default arguments are used.
                    .WithArguments(args)
                    // can tell atlas that in order for my service to run I require that this other service is started
                    // will attempt to start the service if it is not running, there is an overload to provide a time in seconds to wait
                    .WithDependencyOnServiceNamed("MSSQLSERVER");*/
                
                if (args != null && args.Any())
                    configuration = configuration.WithArguments(args);

                Host.Start(configuration);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception during startup.", ex);
                Console.ReadLine();
            }
        }
    }
}
