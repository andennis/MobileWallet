using System.Collections.Generic;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;

namespace Pass.Container.Core
{
    public interface IPassTemplateGenerator
    {
        ClientType ClientType { get; }
        void Generate(GeneralPassTemplate generalTemplate, IEnumerable<string> imageFiles, string dstTemplateFilesPath);
    }
}
