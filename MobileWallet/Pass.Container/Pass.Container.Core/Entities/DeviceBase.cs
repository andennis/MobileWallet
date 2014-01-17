using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core.Entities
{
    public class DeviceBase
    {
        public int DeviceId { get; set; }
        public DeviceType DeviceType { get; set; }
    }
}
