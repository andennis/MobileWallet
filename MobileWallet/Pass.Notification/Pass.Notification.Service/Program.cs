using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas;
using Autofac;
using log4net;
using Pass.Notification.BL.Utils;
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
