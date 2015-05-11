using System;
using Common.Repository;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core.Entities
{
    public class RegistrationInfo
    {
        public int PassId { get; set; }
        public string DeviceId { get; set; }
        public ClientType DeviceType { get; set; }
        public EntityStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
