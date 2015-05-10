using System.Collections.Generic;
using Common.Repository;
using Pass.Notification.Repository.Core.Entities;
using Pass.Notification.Repository.Core.Enums;

namespace Pass.Notification.Repository.Core
{
    public interface IPushNotificationRepository: IRepository<PushNotificationItem>
    {
        void SetPushNotificationStatus(List<int> pushNatificationIds, PushStatus status);
        void ClearPushNotification();
    }
}
