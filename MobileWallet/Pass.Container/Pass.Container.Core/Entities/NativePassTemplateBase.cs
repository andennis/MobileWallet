using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core.Entities
{
    public class NativePassTemplateBase
    {
        public int NativePassTemplateId { get; set; }
        public PassTemplate Template { get; set; }
        public PassTemplateType TemplateType { get; set; }
    }
}
