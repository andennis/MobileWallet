using Common.Configuration;
using Pass.Distribution.Core;

namespace Pass.Distribution.BL
{
    public class PassDistributionConfig : AppDbConfig, IPassDistributionConfig
    {
        public PassDistributionConfig()
            : base("PassDistribution")
        {
        }

        public string SecurityKey
        {
            get { return GetValue("SecurityKey"); }
        }
    }
}
