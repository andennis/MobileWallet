using System;
using System.Collections.Generic;
using Common.Repository.EF;
using Pass.Notification.Repository.Core;
using Pass.Notification.Repository.Core.Entities;

namespace Pass.Notification.Repository.EF
{
    public sealed class PushNotificationRepository : Repository<PushNotificationItem>, IPushNotificationRepository
    {
        public PushNotificationRepository(DbContextBase dbContext)
            : base(dbContext)
        {
        }

       

        public void ClearPushNotification()
        {
            //using (var trn = new TransactionScope())
            {
                ExecuteCommand(string.Format("DELETE FROM {0}.PushNotificationItem", DbScheme));
                //trn.Complete();
            }
        }
    }
}
