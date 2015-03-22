using System.Collections.Generic;
using NUnit.Framework;
using Pass.Notification.BL;
using Pass.Notification.Core;
using Pass.Notification.Repository.Core.Enums;

namespace Pass.Notification.Tests
{
    public class PassNotificationServiceTests
    {
        private PassNotificationService _passNotificationService;
        private readonly IPushNotificationConfig _pnConfig;

        public PassNotificationServiceTests()
        {
            _pnConfig = new PushNotificationConfig();
        }

        private IPassNotificationService GetPassNotificationService()
        {
            return Factory.PassNotificationFactory.Create(_pnConfig);
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
