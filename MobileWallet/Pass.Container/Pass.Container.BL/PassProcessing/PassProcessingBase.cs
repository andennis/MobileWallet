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
            var reg = regRep.Find(passId, clientDevice.ClientDeviceId);
            if (reg != null)
            {
                if (reg.Status != EntityStatus.Active)
                {
                    reg.Status = EntityStatus.Active;
                    regRep.Update(reg);
                    //_pcUnitOfWork.Save();
                    return PassProcessingStatus.Succeed;
                }
                return PassProcessingStatus.AlreadyDone;
            }

            reg = new Registration()
                      {
                          PassId = passId,
                          ClientDeviceId = clientDevice.ClientDeviceId,
                          Status = EntityStatus.Active
                      };

            regRep.Insert(reg);
            //_pcUnitOfWork.Save();
            return PassProcessingStatus.Succeed;
        }
        protected PassProcessingStatus RemovePassFromClientDevice(int passId, ClientDevice clientDevice)
        {
            IRepository<Registration> regRep = _pcUnitOfWork.GetRepository<Registration>();
            var reg = regRep.Find(passId, clientDevice.ClientDeviceId);
            if (reg == null || reg.Status != EntityStatus.Active)
                return PassProcessingStatus.AlreadyDone;

            reg.Status = EntityStatus.Deleted;
            regRep.Update(reg);
            //_pcUnitOfWork.Save();
            return PassProcessingStatus.Succeed;
        }

        protected ClientDevice GetClientDevice(string deviceId, ClientDeviceType deviceType)
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
