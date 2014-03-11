using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;

namespace Pass.Container.Core
{
    public interface INativePassTemplateGenerator
    {
        ClientType ClientType { get; }
        void Generate(GeneralPassTemplate passTemplate, string storageItemPath);
    }
}
