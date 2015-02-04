using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Notification.Core;
using Pass.Notification.Repository.Core;

namespace Pass.Notification.BL
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IPushNotificationUnitOfWork _pnUnitOfWork;
        private readonly IPushNotificationWorker _pushNotificationWorker;


        public PushNotificationService(IPushNotificationUnitOfWork pnUnitOfWork, IPushNotificationWorker pushNotificationWorker)
        {
            _pnUnitOfWork = pnUnitOfWork;
            _pushNotificationWorker = pushNotificationWorker;
        }
    }
}
