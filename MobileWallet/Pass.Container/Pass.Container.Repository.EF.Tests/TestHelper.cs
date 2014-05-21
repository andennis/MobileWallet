using Common.Repository;
using Pass.Container.Repository.Core;

namespace Pass.Container.Repository.EF.Tests
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

        public static IPassContainerUnitOfWork GetPassContainerUnitOfWork()
        {
            return new PassContainerUnitOfWork(TestHelper.DbConfig);
        }

    }
}
