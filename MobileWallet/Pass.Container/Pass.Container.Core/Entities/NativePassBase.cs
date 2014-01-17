using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Container.Core.Entities
{
    public class NativePassBase
    {
        public int PackageId { get; set; }
        public PassTemplate PassTemplate { get; set; }
    }
}
