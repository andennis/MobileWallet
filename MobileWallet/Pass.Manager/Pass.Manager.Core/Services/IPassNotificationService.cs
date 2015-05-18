namespace Pass.Manager.Core.Services
{
    public interface IPassNotificationService
    {
        void NotifyClientDevice(int passContentId, int clientDeviceId);
        void NotifyPassClientDevices(int passContentId);
        void NotifyPassTemplateClientDevices(int passContentTemplateId);
    }
}