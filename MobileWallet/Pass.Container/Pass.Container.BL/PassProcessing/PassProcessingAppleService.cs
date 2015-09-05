using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Common.Repository;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.PassProcessing
{
    public class PassProcessingAppleService : PassProcessingBase, IPassProcessingAppleService
    {
        private readonly IPassRepository _repPass;
        private readonly IRepository<ClientDeviceApple> _repClientDeviceApple;
        private readonly IPassService _passService;

        public PassProcessingAppleService(IPassContainerUnitOfWork pcUnitOfWork, IPassService passService)
            :base(pcUnitOfWork)
        {
            _repPass = _pcUnitOfWork.PassRepository;
            _repClientDeviceApple = _pcUnitOfWork.GetRepository<ClientDeviceApple>();
            _passService = passService;
        }

        public PassProcessingStatus RegisterDevice(string deviceLibraryIdentifier, string passTypeId, string serialNumber, string pushToken, string authToken)
        {
            Repository.Core.Entities.Pass pass = _repPass.GetPassApple(serialNumber, passTypeId);
            if (pass == null)
                return PassProcessingStatus.NotFound;
            if (!Authenticate(authToken, pass))
                return PassProcessingStatus.Unauthorized;

            ClientDeviceApple clientDevice = GetClientDevice(deviceLibraryIdentifier);
            if (clientDevice == null)
            {
                clientDevice = new ClientDeviceApple() {DeviceId = deviceLibraryIdentifier, PushToken = pushToken};
                _pcUnitOfWork.GetRepository<ClientDeviceApple>().Insert(clientDevice);
            }

            PassProcessingStatus status = AddPassToClientDevice(pass.PassId, clientDevice);
            _pcUnitOfWork.Save();
            return status;
        }
        public PassProcessingStatus UnregisterDevice(string deviceLibraryIdentifier, string passTypeId, string serialNumber, string authToken)
        {
            Repository.Core.Entities.Pass pass = _repPass.GetPassApple(serialNumber, passTypeId);
            if (pass == null)
                return PassProcessingStatus.NotFound;
            if (!Authenticate(authToken, pass))
                return PassProcessingStatus.Unauthorized;

            ClientDeviceApple clientDevice = GetClientDevice(deviceLibraryIdentifier);
            if (clientDevice == null)
                return PassProcessingStatus.NotFound;

            RemovePassFromClientDevice(pass.PassId, clientDevice);
            _pcUnitOfWork.Save();
            return PassProcessingStatus.Succeed;
        }

        public PassProcessingStatus GetChangedPasses(string deviceLibraryIdentifier, string passTypeId, DateTime? passesUpdatedSince, out ChangedPassesInfo changedPassesInfo)
        {
            changedPassesInfo = null;
            ClientDevice device = GetClientDevice(deviceLibraryIdentifier, ClientType.Apple);
            if (device == null)
                return PassProcessingStatus.NotFound;

            IList<ChangedPass> changedPasses = _pcUnitOfWork.PassRepository.GetChangedPassesApple(deviceLibraryIdentifier, passTypeId, passesUpdatedSince);
            if (!changedPasses.Any())
                return PassProcessingStatus.NoContent;

            changedPassesInfo = new ChangedPassesInfo()
                                    {
                                        SerialNumbers = changedPasses.Select(x => x.SerialNumber).ToList(),
                                        LastUpdated = changedPasses.Max(x => x.UpdatedDate)
                                    };

            return PassProcessingStatus.Succeed;
        }

        public PassProcessingStatus GetPassPackage(string passTypeId, string serialNumber, string authToken, DateTime? lastUpdatedDate, out PassPackageInfo packageInfo)
        {
            packageInfo = null;
            Repository.Core.Entities.Pass pass = _repPass.GetPassApple(serialNumber, passTypeId);
            if (pass == null)
                return PassProcessingStatus.NotFound;
            if (!Authenticate(authToken, pass))
                return PassProcessingStatus.Unauthorized;

            var pt = _pcUnitOfWork.GetRepository<PassTemplate>().Find(pass.PassTemplateId);
            DateTime updatedDate = pass.UpdatedDate >= pt.UpdatedDate ? pass.UpdatedDate : pt.UpdatedDate;
            updatedDate = updatedDate.TruncateMiliseconds();
            if (lastUpdatedDate.HasValue && lastUpdatedDate.Value >= updatedDate)
                return PassProcessingStatus.NotModified;

            string packagePath = _passService.GetPassPackage(pass.PassId, ClientType.Apple);
            packageInfo = new PassPackageInfo()
                          {
                              PackageFilePath = packagePath,
                              UpdatedDate = updatedDate
                          };

            return PassProcessingStatus.Succeed;
        }

        public void Log(string logMessage)
        {
            throw new NotImplementedException();
        }

        private ClientDeviceApple GetClientDevice(string deviceId)
        {
            return _repClientDeviceApple.Query()
                .Filter(x => x.DeviceId == deviceId && x.DeviceType == ClientType.Apple)
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
