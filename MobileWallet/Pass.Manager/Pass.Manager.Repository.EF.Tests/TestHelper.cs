﻿using System;
using Common.Repository;
using Pass.Manager.Core.Entities;
using Pass.Manager.Repository.Core;

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

        public static PassSite GetNewPassSite()
        {
            return new PassSite()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };
        }

        public static User GetNewUser()
        {
            return new User()
                   {
                       UserName = Guid.NewGuid().ToString(),
                       FirstName = "FN" + Guid.NewGuid().ToString(),
                       LastName = "LN" + Guid.NewGuid().ToString()
                   };
        }

        public static PassCertificateApple GetNewAppleCertificate()
        {
            return new PassCertificateApple()
                   {
                       Name = Guid.NewGuid().ToString(),
                       ExpDate = DateTime.Now.Date,
                       PassTypeId = "PType1",
                       TeamId = "Team1",
                       CertificateStorageId = 111
                   };
        }

    }
}
