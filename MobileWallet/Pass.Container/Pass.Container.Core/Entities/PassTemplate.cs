using System.Collections.Generic;

namespace Pass.Container.Core.Entities
{
    public class PassTemplate
    {
        public int PassTemplateId { get; set; }
        public string Name { get; set; }
        public int PackageId { get; set; }
        public ICollection<Pass> Passes { get; set; }
        public ICollection<PassField> PassFields { get; set; }
        public ICollection<NativePassTemplateBase> NativeTemplates  { get; set; }
    }
}
