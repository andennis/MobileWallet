using System.Collections.Generic;
using Common.Repository;

namespace Pass.Container.Repository.Core.Entities
{
    public sealed class PassTemplate : EntityVersionable
    {
        public int PassTemplateId { get; set; }
        public string Name { get; set; }
        public int PackageId { get; set; }
        public EntityStatus Status { get; set; }
        public ICollection<Pass> Passes { get; set; }
        public ICollection<PassField> PassFields { get; set; }
        public ICollection<PassTemplateNative> NativeTemplates { get; set; }
    }
}
