using System;
using System.Collections.Generic;
using System.IO;
using FileStorage.BL;
using FileStorage.Core;
using FileStorage.Factory;
using NUnit.Framework;
using Pass.Container.BL.PassGenerators;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Factory;
using Pass.Container.Repository.EF;

namespace Pass.Container.BL.Tests.NativePassGenerators
{
    class NativePassGeneratorsTests
    {
        private readonly FileStorageConfig _fsConfig;

        public NativePassGeneratorsTests()
        {
            _fsConfig = new FileStorageConfig();

        }

        [TestFixtureSetUp]
        public void InitAllTests()
        {
            TestHelper.ClearFileStorage(_fsConfig);
        }

        [Test]
        public void CreatePassTest()
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
                int passId = pcs.CreatePass(passTemplateId, fields);

                Assert.Greater(passId, 0);

                var applePassGenerator = new ApplePassGenerator(new PassContainerConfig(),
                                                                new PassContainerUnitOfWork(new PassContainerConfig()),
                                                                fss);

                string pkpassFilePath = applePassGenerator.GeneratePass(passId);
                Assert.NotNull(pkpassFilePath);
            }
        }

        private IPassContainerService GetPassContainerService()
        {
            return PassContainerFactory.CreateContainerService(new PassContainerConfig());
        }

        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.CreateTemplateService(new PassContainerConfig(), _fsConfig);
        }

        private IFileStorageService GetFileStorageService()
        {
            return FileStorageFactory.Create(_fsConfig);
        }
    }
}
