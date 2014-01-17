using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Container.Core.Entities
{
    public class PassTemplate
    {
        public int PassTemplateId { get; set; }
        public string Name { get; set; }
        public int TemplatePackageId { get; set; }
        public ICollection<Pass> Passes { get; set; }
        public ICollection<PassField> PassFields { get; set; }
        public ICollection<NativePassBase> NativePassBaseCollection { get; set; }
    }
}
