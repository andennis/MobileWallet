using Common.Repository;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Repository.Core.Entities
{
    public class PassNative : EntityVersionable
    {
        public int PassNativeId { get; set; }

        public int PassId { get; set; }
        public Pass Pass { get; set; }

        public int? PassFileStorageId { get; set; }
        public ClientType DeviceType { get; set; }

        public int PassTemplateNativeId { get; set; }
        public PassTemplateNative PassTemplateNative { get; set; }
    }
}
