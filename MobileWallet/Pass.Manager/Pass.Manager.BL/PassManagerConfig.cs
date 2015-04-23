using Common.Configuration;
using Pass.Manager.Core;

namespace Pass.Manager.BL
{
    public class PassManagerConfig : AppDbConfig, IPassManagerConfig
    {
        public PassManagerConfig()
            : base("PassManager")
        {
        }

        /*
        public string SecurityKey
        {
            get { return GetValue("SecurityKey"); }
        }
        */

        public string WorkingFolder
        {
            get { return GetValue("WorkingFolder"); }
        }

    }
}
