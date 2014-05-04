using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL
{
    public static class EntityConverter
    {
        public static PassFieldValue PassFieldInfoToRepositoryFieldValue(PassFieldInfo passFieldInfo)
        {
            return new PassFieldValue()
                       {
                           PassFieldId = passFieldInfo.PassFieldId,
                           Value = passFieldInfo.Value,
                           Label = passFieldInfo.Label,
                       };
        }

        public static ClientDeviceType ClientTypeToRepositoryClientDeviceType(ClientType clientType)
        {
            switch (clientType)
            {
                case ClientType.Browser:
                    return ClientDeviceType.Browser;
                case ClientType.Apple:
                    return ClientDeviceType.Apple;
                default:
                    return ClientDeviceType.Unknown;
            }
        }
    }
}
