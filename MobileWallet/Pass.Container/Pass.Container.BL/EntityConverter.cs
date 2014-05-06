using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL
{
    public static class EntityConverter
    {
        public static PassFieldInfo RepositoryFieldValueToPassFieldInfo(PassFieldValue passFieldValue)
        {
            return new PassFieldInfo()
            {
                PassFieldId = passFieldValue.PassFieldId,
                Name = passFieldValue.PassField.Name,
                Label = string.IsNullOrEmpty(passFieldValue.Label) ? passFieldValue.PassField.DefaultLabel : passFieldValue.Label,
                Value = string.IsNullOrEmpty(passFieldValue.Value) ? passFieldValue.PassField.DefaultValue : passFieldValue.Value,
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
