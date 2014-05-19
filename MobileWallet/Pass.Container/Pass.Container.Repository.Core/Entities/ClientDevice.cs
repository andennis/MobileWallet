
using System.Collections.Generic;

namespace Pass.Container.Repository.Core.Entities
{
    public class ClientDevice
    {
        public int ClientDeviceId { get; set; }
        public string DeviceId { get; set; }
        public ClientDeviceType DeviceType { get; set; }
        public ICollection<Registration> PassRegistrations { get; set; }
    }
}
