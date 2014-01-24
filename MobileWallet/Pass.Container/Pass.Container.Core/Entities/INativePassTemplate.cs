using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core.Entities
{
    public interface INativePassTemplate
    {
        int NativePassTemplateId { get; set; }
        int PackageId { get; set; }
        PassTemplate PassTemplate { get; set; }
        PassTemplateType PassTemplateType { get; set; }
    }
}
