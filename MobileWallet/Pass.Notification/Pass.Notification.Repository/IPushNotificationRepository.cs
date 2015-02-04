using System.Collections.Generic;
using Common.Repository;
using Pass.Notification.Repository.Core.Entities;

namespace Pass.Notification.Repository.Core
{
    public interface IPushNotificationRepository: IRepository<PushNotificationItem>
    {
        void ClearFileStorage();
    }
}
