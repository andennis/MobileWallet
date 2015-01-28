using System;

namespace Pass.Notification.Service.Quartz
{
    public interface IPushLogicLayer : IDisposable
    {
        void Run();
    }
}
