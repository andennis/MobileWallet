using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Container.Core.Entities
{
    public class PassField
    {
        public int PassFieldId { get; set; }
        public string Name { get; set; }
        public PassTemplate Pass { get; set; }
        public PassFieldValue PassFieldValue { get; set; }
    }
}
