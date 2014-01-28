using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core.Entities
{
    public class DeviceRegistration
    {
        public int DeviceRegistrationId { get; set; }
        public DeviceRegistrationStatus Status { get; set; }
        public ClientPass Pass { get; set; }
        public DeviceBase DeviceBase { get; set; }
    }
}
