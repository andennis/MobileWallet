using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.GeneralPassTemplate;

namespace Pass.Container.Core
{
    public interface INativePassTemplateGenerator
    {
        PassTemplateType PassTemplateType { get; }
        bool Generate(GeneralPassTemplate passTemplate, string storageItemPath);
    }
}
