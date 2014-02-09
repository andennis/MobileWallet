using System;
using System.IO;
using FileStorage.BL;
using FileStorage.Core;
using FileStorage.Repository.EF;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Repository.EF;

namespace Pass.Container.BL.Tests
{
    public class BlTestsBase
    {
        protected IPassContainerUnitOfWork _pcUnitOfWork;
        protected IFileStorageUnitOfWork _fsUnitOfWork;
        protected IFileStorageService _fsService;
        protected IPassTemplateService _passTemplateService;
        protected string _testPassTemplateDir;

        [SetUp]
        public virtual void InitEachTest()
        {
            _pcUnitOfWork = new PassContainerUnitOfWork(TestHelper.PassContainerConfig);
            IFileStorageConfig fsConfig = new FileStorageConfig();
            _fsUnitOfWork = new FileStorageUnitOfWork(fsConfig);
            _fsService = new FileStorageService(fsConfig, _fsUnitOfWork);
            _passTemplateService = new PassTemplateService(TestHelper.PassContainerConfig, _pcUnitOfWork, _fsService);

            _testPassTemplateDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestPassTemplate");
            if (Directory.Exists(_testPassTemplateDir))
                Directory.Delete(_testPassTemplateDir);
            Directory.CreateDirectory(_testPassTemplateDir);
        }

        [TearDown]
        public virtual void FinalizeEachTest()
        {
            _pcUnitOfWork.Dispose();
            _fsUnitOfWork.Dispose();
        }

    }
}
