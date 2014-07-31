using System;
using Common.Repository;
using Pass.Manager.Repository.Core;
using Pass.Manager.Repository.Core.Entities;

namespace Pass.Manager.Repository.EF.Tests
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

        public static IPassManagerUnitOfWork GetPassManagerUnitOfWork()
        {
            return new PassManagerUnitOfWork(DbConfig);
        }

        public static PassSite CreatePassSite()
        {
            using (var unitOfWork = GetPassManagerUnitOfWork())
            {
                var repPassSite = unitOfWork.GetRepository<PassSite>();
                var passSite = new PassSite()
                {
                    Name = Guid.NewGuid().ToString(),
                    Description = Guid.NewGuid().ToString()
                };
                repPassSite.Insert(passSite);
                unitOfWork.Save();
                return passSite;
            }
        }

    }
}
