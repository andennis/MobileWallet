using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core.Entities
{
    public class NativePassTemplateBase : INativePassTemplate
    {
        public int NativePassTemplateId { get; set; }
        public int PackageId { get; set; }
        public PassTemplateContainer PassTemplate { get; set; }
        public PassTemplateType PassTemplateType { get; set; }
    }
}
