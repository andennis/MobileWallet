using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Container.Core.Entities
{
    public class PassTemplateContainer
    {
        public int PassTemplateId { get; set; }
        public string Name { get; set; }
        public ICollection<ClientPass> Passes { get; set; }
        public ICollection<PassField> PassFields { get; set; }
        public ICollection<NativePassTemplateBase> NativePassTemplates  { get; set; }
    }
}
