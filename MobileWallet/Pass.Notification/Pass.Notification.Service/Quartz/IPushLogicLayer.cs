using System;
using Pass.Notification.Core;

namespace Pass.Notification.Service.Quartz
{
    public interface IPushLogicLayer : IDisposable
    {
        void Run(IPassNotificationService passNotificationService);
    }
}
