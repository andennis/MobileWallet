using System;
using System.Collections.Generic;
using System.Linq;
using Common.Repository;
using Pass.Container.BL.PassInteraction;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using RepEntities = Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL
{
    public class ApplePassProcessingService : PassProcessingBase, IApplePassProcessingService
    {
        private readonly IRepository<RepEntities.Pass> _repPass;
        private readonly IRepository<ClientDeviceApple> _repClientDeviceApple;
        private readonly IPassService _passService;

        public ApplePassProcessingService(IPassContainerUnitOfWork pcUnitOfWork, IPassService passService)
            :base(pcUnitOfWork)
        {
            //_config = config;
            /*
            _fsService = fsService;
            _certService = certService;
            */
            //Repositories
            /*
            _repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            _repTemplateNative = _pcUnitOfWork.GetRepository<PassTemplateNative>();
            _repPassTemplateApple = _pcUnitOfWork.GetRepository<PassTemplateApple>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();
            _repPassFieldValue = _pcUnitOfWork.GetRepository<PassFieldValue>();
            _repRegistration = _pcUnitOfWork.GetRepository<Registration>();
            */
            _repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();
            _repClientDeviceApple = _pcUnitOfWork.GetRepository<ClientDeviceApple>();
            _passService = passService;

            //TODO ApplePassGenerator should be inicialized
            //_passGenerator = new ApplePassGenerator(_config, );
        }

        public void RegisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushToken, string authToken, out PassProcessingStatus status)
        {
            RepEntities.Pass pass = ReadPass(serialNumber, passTypeIdentifier);
            if (pass == null)
            {
                status = PassProcessingStatus.NotFound;
                return;
            }
            if (!Authenticate(authToken, pass))
            {
                status = PassProcessingStatus.Unauthorized;
                return;
            }

            ClientDeviceApple clientDevice = GetClientDevice(deviceLibraryIdentifier);
            if (clientDevice == null)
            {
                clientDevice = new ClientDeviceApple() {DeviceId = deviceLibraryIdentifier, PushToken = pushToken};
                _pcUnitOfWork.GetRepository<ClientDeviceApple>().Insert(clientDevice);
            }

            status = AddPassToClientDevice(pass.PassId, clientDevice);
            _pcUnitOfWork.Save();
        }
        public void UnregisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string authToken, out PassProcessingStatus status)
        {
            RepEntities.Pass pass = ReadPass(serialNumber, passTypeIdentifier);
            if (pass == null)
            {
                status = PassProcessingStatus.NotFound;
                return;
            }
            if (!Authenticate(authToken, pass))
            {
                status = PassProcessingStatus.Unauthorized;
                return;
            }

            ClientDeviceApple clientDevice = GetClientDevice(deviceLibraryIdentifier);
            if (clientDevice == null)
            {
                status = PassProcessingStatus.NotFound;
                return;
            }

            RemovePassFromClientDevice(pass.PassId, clientDevice);
            _pcUnitOfWork.Save();
            status = PassProcessingStatus.Succeed;
        }

        public IList<string> GetSerialNumbersOfPasses(string deviceLibraryIdentifier, string passTypeIdentifier, 
            out DateTime lastUpdated, out PassProcessingStatus status, DateTime? passesUpdatedSince = null)
        {
            lastUpdated = DateTime.Now;
            ClientDevice device = GetClientDevice(deviceLibraryIdentifier, ClientDeviceType.Apple);
            if (device == null)
            {
                status = PassProcessingStatus.NotFound;
                return null;
            }

            status = PassProcessingStatus.Succeed;
            return _pcUnitOfWork.PassRepository.GetPassSerialNumbersApple(deviceLibraryIdentifier, passTypeIdentifier, passesUpdatedSince);
        }

        public string GetPass(string passTypeIdentifier, string serialNumber, string authToken, out PassProcessingStatus status, DateTime? lastUpdatedDate = null)
        {
            RepEntities.Pass pass = ReadPass(serialNumber, passTypeIdentifier);
            if (pass == null)
            {
                status = PassProcessingStatus.NotFound;
                return null;
            }
            if (!Authenticate(authToken, pass))
            {
                status = PassProcessingStatus.Unauthorized;
                return null;
            }

            if (lastUpdatedDate.HasValue && lastUpdatedDate.Value >= pass.UpdatedDate)
            {
                status = PassProcessingStatus.NotModified;
                return null;
            }

            status = PassProcessingStatus.Succeed;
            return _passService.GetPassPackage(pass.PassId, ClientType.Apple);
        }

        private RepEntities.Pass ReadPass(string serialNumber, string passTypeId)
        {
            return _repPass.Query()
                .Filter(x => x.SerialNumber == serialNumber && x.PassTypeId == passTypeId)
                .Get()
                .FirstOrDefault();
        }

        public void Log(string logMessage)
        {
            throw new NotImplementedException();
        }

        private ClientDeviceApple GetClientDevice(string deviceId)
        {
            return _repClientDeviceApple.Query()
                .Filter(x => x.DeviceId == deviceId && x.DeviceType == ClientDeviceType.Apple)
                .Get()
                .FirstOrDefault();
        }

        #region IDisposable
        public void Dispose()
        {
            _pcUnitOfWork.Dispose();
        }
        #endregion

    }
}
