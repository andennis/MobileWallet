namespace Pass.Container.Repository.Core.Entities
{
    public class ClientDeviceApple : ClientDevice
    {
        public ClientDeviceApple()
        {
            DeviceType = ClientDeviceType.Apple;
        }
        public string PushToken { get; set; }
    }
}
