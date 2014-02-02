using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core.Entities
{
    public class Registration
    {
        public int PassId { get; set; }
        public int ClientDeviceId { get; set; }
        public Pass Pass { get; set; }
        public ClientDevice ClientDevice { get; set; }
        public RegistrationStatus Status { get; set; }
    }
}
