﻿using System.Linq;
using Common.Repository;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.PassProcessing
{
    public abstract class PassProcessingBase
    {
        protected readonly IPassContainerUnitOfWork _pcUnitOfWork;

        protected PassProcessingBase(IPassContainerUnitOfWork pcUnitOfWork)
        {
            _pcUnitOfWork = pcUnitOfWork;
        }

        protected PassProcessingStatus AddPassToClientDevice(int passId, ClientDevice clientDevice)
        {
            IRepository<Registration> regRep = _pcUnitOfWork.GetRepository<Registration>();
            Registration reg = regRep.Find(clientDevice.ClientDeviceId, passId);
            if (reg != null)
            {
                if (reg.Status == EntityStatus.Active)
                    return PassProcessingStatus.AlreadyDone;
                
                reg.Status = EntityStatus.Active;
                regRep.Update(reg);
                return PassProcessingStatus.Succeed;
            }

            reg = new Registration()
                      {
                          PassId = passId,
                          ClientDeviceId = clientDevice.ClientDeviceId,
                          Status = EntityStatus.Active
                      };

            regRep.Insert(reg);
            return PassProcessingStatus.Succeed;
        }
        protected PassProcessingStatus RemovePassFromClientDevice(int passId, ClientDevice clientDevice)
        {
            IRepository<Registration> regRep = _pcUnitOfWork.GetRepository<Registration>();
            var reg = regRep.Find(clientDevice.ClientDeviceId, passId);
            if (reg == null || reg.Status != EntityStatus.Active)
                return PassProcessingStatus.AlreadyDone;

            reg.Status = EntityStatus.Inactive;
            regRep.Update(reg);
            return PassProcessingStatus.Succeed;
        }

        protected ClientDevice GetClientDevice(string deviceId, ClientType deviceType)
        {
            return _pcUnitOfWork.GetRepository<ClientDevice>().Query()
                .Filter(x => x.DeviceId == deviceId && x.DeviceType == deviceType)
                .Get()
                .FirstOrDefault();
        }

        protected bool Authenticate(string authToken, Repository.Core.Entities.Pass pass)
        {
            return (pass.AuthToken == authToken);
        }

    }
}
