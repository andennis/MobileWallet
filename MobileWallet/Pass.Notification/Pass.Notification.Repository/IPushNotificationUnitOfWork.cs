using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;

namespace Pass.Notification.Repository.Core
{
    public interface IPushNotificationUnitOfWork : IUnitOfWork
    {
        IPushNotificationRepository PushNotificationRepository { get; }
    }
}
