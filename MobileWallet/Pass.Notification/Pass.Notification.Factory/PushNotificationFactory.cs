using Common.Repository;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Pass.Notification.Core;
using Pass.Notification.Repository.Core;

namespace Pass.Notification.Factory
{
    public class PushNotificationFactory
    {
         private static readonly IUnityContainer _iocContainer = new UnityContainer();

         static PushNotificationFactory()
        {
            _iocContainer.LoadConfiguration("PassNotification");
        }

        public static IPushNotificationService Create(IPushNotificationConfig config = null)
        {
            return _iocContainer.Resolve<IPushNotificationService>();
        }
    }
}
