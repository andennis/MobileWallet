using System.Collections.Generic;
using NUnit.Framework;
using Pass.Notification.BL;
using Pass.Notification.Core;
using Pass.Notification.Repository.Core.Enums;

namespace Pass.Notification.Tests
{
    public class PushNotificationServiceTests
    {
        private PushNotificationService _passNotificationService;
        private readonly IPushNotificationConfig _pnConfig;

        public PushNotificationServiceTests()
        {
            _pnConfig = new PushNotificationConfig();
        }

        private IPushNotificationService GetPassNotificationService()
        {
            return Factory.PushNotificationFactory.Create(_pnConfig);
        }

        [Test]
        public void AddPushNotificationToQueuesTest()
        {
            using (var pnService = GetPassNotificationService())
            {
               pnService.AddPushNotificationToQueue(1, new List<string>(){"1"}, PushNotificationServiceType.Apple);
            }
        }

    }
}
