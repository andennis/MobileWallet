using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Configuration;
using Pass.Manager.Core;

namespace Pass.Manager.BL
{
    public class PassManagerConfig : AppDbConfig, IPassManagerConfig
    {
        public PassManagerConfig()
            : base("PassManagerStorage")
        {
        }

        public string SecurityKey
        {
            get { return GetValue("SecurityKey"); }
        }
    }
}
