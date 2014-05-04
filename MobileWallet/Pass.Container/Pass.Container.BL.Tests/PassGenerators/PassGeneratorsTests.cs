using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CertificateStorage.BL;
using FileStorage.BL;
using FileStorage.Core;
using FileStorage.Factory;
using NUnit.Framework;
using Pass.Container.BL.PassGenerators;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Factory;
using Pass.Container.Repository.EF;

namespace Pass.Container.BL.Tests.PassGenerators
{
    public class PassGeneratorsTests
    {
        private readonly FileStorageConfig _fsConfig;

        public PassGeneratorsTests()
        {
            _fsConfig = new FileStorageConfig();

        }

        [TestFixtureSetUp]
        public void InitAllTests()
        {
            //TestHelper.ClearFileStorage(_fsConfig);
        }

        [Test]
        public void CreatePassTest()
        {
            //using (var fss = GetFileStorageService())
            using (var pts = GetPassTemplateService())
            using (var pcs = GetPassContainerService())
            {
                string testPassTemplateDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestPassTemplate");
                TestHelper.PreparePassTemplateSource(testPassTemplateDir, "template.xml");
                int passTemplateId = pts.CreatePassTemlate(testPassTemplateDir);

                IList<PassFieldInfo> fields = pts.GetPassTemplateFields(passTemplateId);
                foreach (PassFieldInfo passFieldInfo in fields)
                {
                    passFieldInfo.Value = "DynamicField";
                }

                int passId = pcs.CreatePass(passTemplateId, fields);

                Assert.Greater(passId, 0);

                /*
                var applePassGenerator = new ApplePassGenerator(new PassContainerConfig(),
                                                                new PassContainerUnitOfWork(new PassContainerConfig()),
                                                                fss);
                */

                string pkpassFilePath = pcs.GetPassPackage(passId, ClientType.Apple);
                Assert.NotNull(pkpassFilePath);
            }
        }

        /*
        //[Test]
        public void Create10000PassTest()
        {
            using (var fss = GetFileStorageService())
            using (var pts = GetPassTemplateService())
            using (var pcs = GetPassContainerService())
            {
                string testPassTemplateDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestPassTemplate");
                TestHelper.PreparePassTemplateSource(testPassTemplateDir, TestHelper.PassContainerConfig.PassTemplateFileName);
                int passTemplateId = pts.CreatePassTemlate(testPassTemplateDir);

                IList<PassFieldInfo> fields = pts.GetPassTemplateFields(passTemplateId);
                foreach (PassFieldInfo passFieldInfo in fields)
                {
                    passFieldInfo.Value = "Test";
                }

                var passIds = new List<int>();
                for (int i = 0; i < 10000; i++)
                {
                    passIds.Add(pcs.CreatePass(passTemplateId, fields));
                }

                var applePassGenerator = new ApplePassGenerator(new PassContainerConfig(),
                                                                new PassContainerUnitOfWork(new PassContainerConfig()),
                                                                fss);
                Trace.TraceInformation(DateTime.Now.ToString());
                for (int i = 0; i < 10000; i++)
                {
                    string pkpassFilePath = applePassGenerator.GeneratePass(passIds[i]);

                }
                Trace.TraceInformation(DateTime.Now.ToString());
                // Assert.NotNull(pkpassFilePath);
            }
        }
        */

        private IPassContainerService GetPassContainerService()
        {
            return PassContainerFactory.CreateContainerService(new PassContainerConfig());
        }

        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.CreateTemplateService(null);
        }

        private IFileStorageService GetFileStorageService()
        {
            return FileStorageFactory.Create(_fsConfig);
        }
    }
}
