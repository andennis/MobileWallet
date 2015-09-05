using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using FileStorage.BL;
using FileStorage.Core;
using FileStorage.Factory;
using NUnit.Framework;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Factory;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.Tests
{
    [TestFixture]
    public class ApplePassProcessingServiceTests
    {
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly string _testPassTemplateDir;
        private readonly string _passTemplateFileName;

        //Repositories
        //private IRepository<PassTemplate> _repPassTemplate;
        //private IRepository<PassTemplateApple> _repPassTemplateApple;
        private readonly IRepository<PassField> _repPassField;

        /*
        private string _deviceLibraryIdentifier;
        private string _passTypeIdentifier;
        private string _serialNumber;
        private string _pushToken;
        private string _authToken;
        */

        public ApplePassProcessingServiceTests()
        {
            _pcUnitOfWork = TestHelper.PassContainerUnitOfWork;
            _testPassTemplateDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestPassTemplate");
            _passTemplateFileName = "template.xml";

            //Repositories
            //_repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            //_repPassTemplateApple = _pcUnitOfWork.GetRepository<PassTemplateApple>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();
        }

        [Test]
        public void RegisterDeviceTest()
        {
            throw new NotImplementedException();
        }
        [Test]
        public void UnregisterDeviceTest()
        {
            throw new NotImplementedException();
        }
        [Test]
        public void GetSerialNumbersOfPassesTest()
        {
            throw new NotImplementedException();
        }
        [Test]
        public void GetPassTest()
        {
            throw new NotImplementedException();
        }
        [Test]
        public void LogTest()
        {
            throw new NotImplementedException();
        }

        private void CreatePass()
        {
            throw new NotImplementedException();

            int passTemplateId;
            using (var passTemplateService = GetPassTemplateService())
            {
                TestHelper.PreparePassTemplateSource(_testPassTemplateDir, _passTemplateFileName);
                passTemplateId = passTemplateService.CreatePassTemlate(_testPassTemplateDir);
                Assert.Greater(passTemplateId, 0);
            }

            IQueryable<PassField> passFields = _repPassField.Query().Filter(x => x.PassTemplateId == passTemplateId).Get();
            IList<PassFieldInfo> fieldValues = passFields.Select(x => new PassFieldInfo{
                                                                                       PassFieldId = x.PassFieldId, 
                                                                                       Name = x.Name,
                                                                                       Value = x.PassFieldId + "_Value"
                                                                                   }).ToList();
            
        }

        private IPassTemplateService GetPassTemplateService()
        {
            return PassContainerFactory.CreateTemplateService(null, null);
        }

        private IPassProcessingAppleService GetAppleDevicePassProcessingService()
        {
            return PassContainerFactory.CreateApplePassProcessingService(new PassContainerConfig(), new FileStorageConfig());
        }

    }
}
