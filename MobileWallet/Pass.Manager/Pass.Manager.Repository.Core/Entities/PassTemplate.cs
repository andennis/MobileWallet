using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Manager.Repository.Core.Entities
{
    public class PassTemplate : EntityVersionable
    {
        public string Name { get; set; }
    }
}
