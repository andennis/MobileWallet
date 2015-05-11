using Pass.Container.Core.Entities;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL
{
    public static class EntityConverter
    {
        public static PassFieldInfo RepositoryFieldValueToPassFieldInfo(PassFieldValue passFieldValue, bool defultIfNull)
        {
            return new PassFieldInfo()
                    {
                        PassFieldId = passFieldValue.PassFieldId,
                        Name = passFieldValue.PassField.Name,
                        Label = passFieldValue.Label ?? (defultIfNull ? passFieldValue.PassField.DefaultLabel : null),
                        Value = passFieldValue.Value ?? (defultIfNull ? passFieldValue.PassField.DefaultValue : null)
                    };
        }

        /*
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
        */
    }
}
