using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Entities.Templates.PassTemplate;

namespace Pass.Container.BL.NativePassTemplateGenerators
{
    public class AppleNativePassTemplateGenerator : INativePassTemplateGenerator
    {
        public PassTemplateType PassTemplateType
        {
            get { return PassTemplateType.AppleTemplate; }
        }

        public bool Generate(PassTemplate passTemplate, string storageItemPath)
        {
            throw new NotImplementedException();
        }
    }
}
