
using System.Collections.Generic;

namespace Pass.Container.Core.Entities
{
    public class ClientDevice
    {
        public int ClientDeviceId { get; set; }
        public string DeviceId { get; set; }
        public ICollection<Registration> PassRegistrations { get; set; }
    }
}
