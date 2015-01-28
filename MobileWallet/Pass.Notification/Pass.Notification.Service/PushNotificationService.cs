using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Notification.Service
{
    [ServiceContract]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PushNotificationService
    {
        [OperationContract]
        public bool SendNotifications(int certificateStorageId, IEnumerable<string> pushTockenIds)
        {
            return true;
        }
    }
}
