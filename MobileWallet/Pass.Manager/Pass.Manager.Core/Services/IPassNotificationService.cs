namespace Pass.Manager.Core.Services
{
    public interface IPassNotificationService
    {
        void NotifyClientDevice(int passContentId, int clientDeviceId);
        void NotifyClientDevices(int passContentId);
        void NotifyTemplateClientDevices(int passContentTemplateId);
    }
}