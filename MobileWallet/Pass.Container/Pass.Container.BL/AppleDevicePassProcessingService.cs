using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using FileStorage.Core;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass = Pass.Container.Core.Entities.Pass;

namespace Pass.Container.BL
{
    public class AppleDevicePassProcessingService : IAppleDevicePassProcessingService
    {
        private readonly IPassTemplateConfig _ptConfig;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IFileStorageService _fsService;
        //Repositories
        private readonly IRepository<PassTemplate> _repPassTemplate;
        private readonly IRepository<PassTemplateNative> _repTemplateNative;
        private readonly IRepository<PassTemplateApple> _repPassTemplateApple;
        private readonly IRepository<PassField> _repPassField;
        private readonly IRepository<Core.Entities.Pass> _repPass;
        private readonly IRepository<Registration> _repRegistration;
        private readonly IRepository<ClientDeviceApple> _repClientDeviceApple;

        public AppleDevicePassProcessingService(IPassTemplateConfig config, IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService)
        {
            _ptConfig = config;
            _pcUnitOfWork = pcUnitOfWork;
            _fsService = fsService;

            //Repositories
            _repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            _repTemplateNative = _pcUnitOfWork.GetRepository<PassTemplateNative>();
            _repPassTemplateApple = _pcUnitOfWork.GetRepository<PassTemplateApple>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();
            _repPass = _pcUnitOfWork.GetRepository<Core.Entities.Pass>();
            _repRegistration = _pcUnitOfWork.GetRepository<Registration>();
            _repClientDeviceApple = _pcUnitOfWork.GetRepository<ClientDeviceApple>();
        }

        public void RegisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushToken, string authToken, out PassProcessingStatus status)
        {
            //Authenticate
            bool isAuthenticate = Authenticate(authToken);
            if (!isAuthenticate)
            {
                status = PassProcessingStatus.Unauthorized;
                return;
            }

            //Find pass
            Core.Entities.Pass pass = _repPass.Query()
                .Filter(x => x.SerialNumber == serialNumber && x.AuthToken == authToken && x.PassTypeIdentifier == passTypeIdentifier)
                .Include(x => x.PassRegistrations)
                .Get()
                .FirstOrDefault();

            if (pass == null)
            {
                status = PassProcessingStatus.NotFound;
                return;
            }

            //Find the same device registration
            ClientDeviceApple device = _repClientDeviceApple.Query()
                                 .Filter(x => x.DeviceId == deviceLibraryIdentifier && x.PushToken == pushToken)
                                 .Include(x => x.PassRegistrations)
                                 .Get()
                                 .FirstOrDefault();
            if (device != null)
            {
                //Check device already contains pass in PassRegistrations 
                if (device.PassRegistrations.Any(registration => registration.PassId == pass.PassId))
                {
                    status = PassProcessingStatus.AlreadyDone;
                    return;
                }
                else
                {
                    //Add new pass registration into existing device
                    var registration = new Registration
                        {
                            ClientDeviceId = device.ClientDeviceId,
                            PassId = pass.PassId,
                            Status = RegistrationStatus.Active
                        };
                    device.PassRegistrations.Add(registration);
                    _repClientDeviceApple.Update(device);
                    _pcUnitOfWork.Save();
                    status = PassProcessingStatus.Succeed;
                    return;
                }
            }

            //Register client device 
            var clientDeviceApple = new ClientDeviceApple
                {
                    DeviceId = deviceLibraryIdentifier,
                    PushToken = pushToken
                };
            //Add new pass registration into new device
            var registrationPass = new Registration
            {
                ClientDeviceId = clientDeviceApple.ClientDeviceId,
                PassId = pass.PassId,
                Status = RegistrationStatus.Active
            };
            clientDeviceApple.PassRegistrations.Add(registrationPass);
            _repClientDeviceApple.Insert(clientDeviceApple);
            _pcUnitOfWork.Save();
            status = PassProcessingStatus.Succeed;
        }

        public void UnregisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushToken, string authToken, out PassProcessingStatus status)
        {
            //Authenticate
            bool isAuthenticate = Authenticate(authToken);
            if (!isAuthenticate)
            {
                status = PassProcessingStatus.Unauthorized;
                return;
            }

            //Find pass
            Core.Entities.Pass pass = _repPass.Query()
                .Filter(x => x.SerialNumber == serialNumber && x.AuthToken == authToken && x.PassTypeIdentifier == passTypeIdentifier)
                .Include(x => x.PassRegistrations)
                .Get()
                .FirstOrDefault();

            if (pass == null)
            {
                status = PassProcessingStatus.NotFound;
                return;
            }

            //Find the same device registration
            ClientDeviceApple device = _repClientDeviceApple.Query()
                                 .Filter(x => x.DeviceId == deviceLibraryIdentifier && x.PushToken == pushToken)
                                 .Include(x => x.PassRegistrations)
                                 .Get()
                                 .FirstOrDefault();
            if (device == null)
            {
                status = PassProcessingStatus.NotFound;
                return;
            }

            //Check pass contains device in PassRegistrations 
            Registration registration = pass.PassRegistrations.FirstOrDefault(x => x.ClientDeviceId == device.ClientDeviceId);
            if (registration == null)
            {
                bool deleteDevice = CheckDeviceShouldBeDeleted(device, pass.PassId);
                if (deleteDevice)
                {
                    _repClientDeviceApple.Delete(device);
                    _pcUnitOfWork.Save();
                }
                status = PassProcessingStatus.AlreadyDone;
            }
            else
            {
                pass.PassRegistrations.Remove(registration);
                _repPass.Update(pass);

                bool deleteDevice = CheckDeviceShouldBeDeleted(device, pass.PassId);
                if (deleteDevice)
                    _repClientDeviceApple.Delete(device);
                _pcUnitOfWork.Save();
                status = PassProcessingStatus.Succeed;
            }
        }

        public IList<string> GetSerialNumbersOfPasses(string deviceLibraryIdentifier, string passTypeIdentifier, ref DateTime lastUpdated, out PassProcessingStatus status, DateTime? passesUpdatedSince = null)
        {
            ClientDeviceApple device = _repClientDeviceApple.Query()
                                                            .Filter(x => x.DeviceId == deviceLibraryIdentifier)
                                                            .Include(x => x.PassRegistrations)
                                                            .Get()
                                                            .FirstOrDefault();
            if (device == null)
            {
                status = PassProcessingStatus.NotFound;
                return null;
            }

            IEnumerable<int> passIdsOfDevice = device.PassRegistrations.Select(x => x.PassId);
            IQueryable<Core.Entities.Pass> passesOfDevice = _repPass.Query()
                                                                    .Filter(x => passIdsOfDevice.Contains(x.PassId) && x.PassTypeIdentifier == passTypeIdentifier)
                                                                    .Get();
            if (!passIdsOfDevice.Any())
            {
                status = PassProcessingStatus.NotFound;
                return null;
            }

            var lastUpdatedTime = new DateTime();
            var updatedPasses = new List<string>();
            foreach (Core.Entities.Pass pass in passesOfDevice)
            {
                if (passesUpdatedSince == null || pass.UpdatedDate > passesUpdatedSince)
                {
                    lastUpdatedTime = pass.UpdatedDate;
                    updatedPasses.Add(pass.SerialNumber);
                }
            }
            if (!updatedPasses.Any())
            {
                status = PassProcessingStatus.NoContent;
                lastUpdated = lastUpdatedTime;
                return null;
            }

            status = PassProcessingStatus.Succeed;
            lastUpdated = lastUpdatedTime;
            return updatedPasses;
        }

        public void GetPass(string passTypeIdentifier, string serialNumber, string authToken, out PassProcessingStatus status)
        {
            //Authenticate
            bool isAuthenticate = Authenticate(authToken);
            if (!isAuthenticate)
            {
                status = PassProcessingStatus.Unauthorized;
                return;
            }

            //Find pass
            Core.Entities.Pass pass = _repPass.Query()
                .Filter(x => x.SerialNumber == serialNumber && x.PassTypeIdentifier == passTypeIdentifier)
                .Get()
                .FirstOrDefault();

            if (pass == null)
            {
                status = PassProcessingStatus.NotFound;
                return;
            }

            //Todo generate pass
            GetPass(pass);
            status = PassProcessingStatus.Succeed;
            return;
        }

        public void Log(string logMessage)
        {
            throw new NotImplementedException();
        }

        private bool Authenticate(string authToken)
        {
            //TODO authenticate by authToken
            return true;
        }

        private bool CheckDeviceShouldBeDeleted(ClientDeviceApple device, int passId)
        {
            if (device.PassRegistrations.Any(x => x.PassId != passId))
                return false;
            return true;
        }

        private void GetPass(Core.Entities.Pass pass)
        {
            //TODO generate Pass
        }

    }
}
