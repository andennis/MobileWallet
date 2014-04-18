using Common.Repository;

namespace Pass.CertificateStorage.Repository.EF.Tests
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

        public static IDbConfig DbConfig{get{return new TestDbConfig();}}
    }
}
