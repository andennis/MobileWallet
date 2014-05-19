using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using RepEntities = Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.PassInteraction
{
    public abstract class PassInteractionBase
    {
        protected readonly IPassContainerUnitOfWork _pcUnitOfWork;

        protected PassInteractionBase(IPassContainerUnitOfWork pcUnitOfWork)
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
            var reg = regRep.Find(passId, clientDevice.DeviceId);
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

        protected bool Authenticate(string authToken, RepEntities.Pass pass)
        {
            return (pass.AuthToken == authToken);
        }

    }
}
