using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Repository.Core.Entities
{
    public class ClientDeviceApple : ClientDevice
    {
        public ClientDeviceApple()
        {
            DeviceType = ClientType.Apple;
        }
        public string PushToken { get; set; }
    }
}
