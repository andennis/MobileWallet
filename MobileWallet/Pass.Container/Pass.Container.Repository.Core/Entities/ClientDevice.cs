using System.Collections.Generic;
using Common.Repository;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Repository.Core.Entities
{
    public class ClientDevice : EntityVersionable
    {
        public int ClientDeviceId { get; set; }
        public string DeviceId { get; set; }
        public ClientType DeviceType { get; set; }
        public ICollection<Registration> PassRegistrations { get; set; }
    }
}
