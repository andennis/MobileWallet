using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Repository;
using Pass.Container.BL.PassGenerators;
using Pass.Container.BL.PassInteraction;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using RepEntities = Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL
{
    public class ApplePassProcessingService : PassInteractionBase, IApplePassProcessingService
    {
        //private readonly IPassContainerConfig _config;
        /*
        private readonly IFileStorageService _fsService;
        private readonly IPassCertificateService _certService;
        */

        //Repositories
        /*
        private readonly IRepository<PassTemplate> _repPassTemplate;
        private readonly IRepository<PassTemplateNative> _repTemplateNative;
        private readonly IRepository<PassTemplateApple> _repPassTemplateApple;
        private readonly IRepository<PassField> _repPassField;
        private readonly IRepository<PassFieldValue> _repPassFieldValue;
        private readonly IRepository<Registration> _repRegistration;
        */
        private readonly IRepository<RepEntities.Pass> _repPass;
        private readonly IRepository<ClientDeviceApple> _repClientDeviceApple;
        private readonly IPassService _passService;
        //private readonly ApplePassGenerator _passGenerator;

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
            RepEntities.Pass pass = GetPass(serialNumber, passTypeIdentifier);
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

            /*
            if (clientDevice != null)
            {
                //Check device already contains pass in PassRegistrations 
                if (clientDevice.PassRegistrations.Any(registration => registration.PassId == pass.PassId))
                {
                    status = PassProcessingStatus.AlreadyDone;
                    return;
                }
                else
                {
                    //Add new pass registration into existing device
                    var registration = new Registration
                        {
                            ClientDeviceId = clientDevice.ClientDeviceId,
                            PassId = pass.PassId,
                            Status = EntityStatus.Active
                        };
                    clientDevice.PassRegistrations.Add(registration);
                    _repClientDeviceApple.Update(clientDevice);
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
                Status = EntityStatus.Active
            };
            clientDeviceApple.PassRegistrations.Add(registrationPass);
            _repClientDeviceApple.Insert(clientDeviceApple);
            _pcUnitOfWork.Save();
            status = PassProcessingStatus.Succeed;
            */
        }
        public void UnregisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string authToken, out PassProcessingStatus status)
        {
            RepEntities.Pass pass = GetPass(serialNumber, passTypeIdentifier);
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

            /*
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
            */
        }

        public IList<string> GetSerialNumbersOfPasses(string deviceLibraryIdentifier, string passTypeIdentifier, out DateTime lastUpdated, out PassProcessingStatus status, DateTime? passesUpdatedSince = null)
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

            /*
            IEnumerable<int> passIdsOfDevice = device.PassRegistrations.Select(x => x.PassId);
            IQueryable<RepEntities.Pass> passesOfDevice = _repPass.Query()
                .Filter(x => passIdsOfDevice.Contains(x.PassId) && x.PassTypeId == passTypeIdentifier)
                .Get();

            if (!passIdsOfDevice.Any())
            {
                status = PassProcessingStatus.NotFound;
                return null;
            }

            var lastUpdatedTime = new DateTime();
            var updatedPasses = new List<string>();
            foreach (RepEntities.Pass pass in passesOfDevice)
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
            */
        }

        public string GetPass(string passTypeIdentifier, string serialNumber, string authToken, out PassProcessingStatus status)
        {
            RepEntities.Pass pass = GetPass(serialNumber, passTypeIdentifier);
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

            status = PassProcessingStatus.Succeed;
            return _passService.GetPassPackage(pass.PassId, ClientType.Apple);
        }

        public void Log(string logMessage)
        {
            throw new NotImplementedException();
        }


        private RepEntities.Pass GetPass(string serialNumber, string passTypeId)
        {
            return _repPass.Query()
                .Filter(x => x.SerialNumber == serialNumber && x.PassTypeId == passTypeId)
                .Get()
                .FirstOrDefault();
        }

        private ClientDeviceApple GetClientDevice(string deviceId)
        {
            return _repClientDeviceApple.Query()
                .Filter(x => x.DeviceId == deviceId && x.DeviceType == ClientDeviceType.Apple)
                .Get()
                .FirstOrDefault();
        }

        /*
        private bool CheckDeviceShouldBeDeleted(ClientDeviceApple device, int passId)
        {
            if (device.PassRegistrations.Any(x => x.PassId != passId))
                return false;
            return true;
        }
        */
       
        #region IDisposable
        public void Dispose()
        {
            _pcUnitOfWork.Dispose();
        }
        #endregion

    }
}
