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

        public string WorkingFolder
        {
            get { return GetValue("WorkingFolder"); }
        }

        public string WebDistributionUrl
        {
            get { return GetValue("WebDistributionUrl"); }
        }

    }
}
