using System;
using System.Collections.Generic;
using System.Linq;
using Common.Repository.EF;
using Pass.Notification.Repository.Core;
using Pass.Notification.Repository.Core.Entities;
using Pass.Notification.Repository.Core.Enums;

namespace Pass.Notification.Repository.EF
{
    public sealed class PushNotificationRepository : Repository<PushNotificationItem>, IPushNotificationRepository
    {
        public PushNotificationRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }

        public void SetPushNotificationStatus(List<int> pushNatificationIds, PushStatus status)
        {
            if (pushNatificationIds == null || pushNatificationIds.Count == 0)
                return;
            string strPushNatificationIds = String.Join(",", pushNatificationIds);
            string command = string.Format("UPDATE {0}.PushNotificationItem " +
                                           "SET Status = {1} " +
                                           "WHERE PushNotificationItemId IN ({2})",
                                            DbScheme, (int) status, strPushNatificationIds);

            ExecuteCommand(command);
        }

        public void ClearPushNotification()
        {
            ExecuteCommand(string.Format("DELETE FROM {0}.PushNotificationItem", DbScheme));
        }
    }
}
