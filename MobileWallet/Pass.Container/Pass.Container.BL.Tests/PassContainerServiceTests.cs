﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileStorage.BL;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Factory;
using Pass.Container.Repository.Core.Entities;


namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class PassContainerServiceTests
    {
        private readonly FileStorageConfig _fsConfig;

        public PassContainerServiceTests()
        {
            _fsConfig = new FileStorageConfig();
        }

        [TestFixtureSetUp]
        public void InitAllTests()
        {
            TestHelper.ClearFileStorage(_fsConfig);
        }

        [Test]
        public void DisposeTest()
        {
            IPassContainerService pcs = GetPassContainerService();
            Assert.IsInstanceOf<IDisposable>(pcs);
            Assert.DoesNotThrow(pcs.Dispose);
        }

        [Test]
        public void CreatePassTest()
        {
            using (var pts = GetPassTemplateService())
            using (var pcs = GetPassContainerService())
            {
                string testPassTemplateDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestPassTemplate");
                TestHelper.PreparePassTemplateSource(testPassTemplateDir, "template.xml");
                int passTemplateId = pts.CreatePassTemlate(testPassTemplateDir);

                IList<PassFieldInfo> fields = pts.GetPassTemplateFields(passTemplateId);
                int passId = pcs.CreatePass(passTemplateId, fields);
                Assert.Greater(passId, 0);
                //TODO not finished
            }
        }

        [Test]
        public void UpdatePassFieldsTest()
        {
            throw new NotImplementedException();
        }

        private IPassContainerService GetPassContainerService()
        {
            return PassContainerFactory.CreateContainerService(new PassContainerConfig());
        }

        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.CreateTemplateService(null);
        }

    }
}
