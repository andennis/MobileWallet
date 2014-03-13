using Common.Repository;

namespace Pass.Container.Repository.Core.Entities
{
    public class Registration
    {
        public int PassId { get; set; }
        public int ClientDeviceId { get; set; }
        public Pass Pass { get; set; }
        public ClientDevice ClientDevice { get; set; }
        public EntityStatus Status { get; set; }
    }
}
