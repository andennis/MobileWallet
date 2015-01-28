using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;

namespace Pass.Notification.Repository.EF.Tests
{
    public static class TestHelper
    {
        private class TestDbConfig : IDbConfig
        {
            public string ConnectionString
            {
                get { return "MobileWalletConnection"; }
            }
        }

        public static IDbConfig DbConfig { get { return new TestDbConfig(); } }
    }
}
