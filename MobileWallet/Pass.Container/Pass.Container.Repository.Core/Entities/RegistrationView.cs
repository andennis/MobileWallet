using Common.Repository;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Repository.Core.Entities
{
    public class RegistrationView
    {
        public int PassId { get; set; }
        public int ClientDeviceId { get; set; }
        public ClientDevice ClientDevice { get; set; }
        public EntityStatus Status { get; set; }

        public string DeviceId { get; set; }
        public ClientType DeviceType { get; set; }
        public string PushToken { get; set; }
    }
}
