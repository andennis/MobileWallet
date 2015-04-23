using Common.Configuration;
using Pass.Container.Core;

namespace Pass.Container.BL
{
    public sealed class PassContainerConfig : AppDbConfig, IPassContainerConfig
    {
        public PassContainerConfig()
            : base("PassContainer")
        {
        }

        #region IPassTemplateConfig
        public string PassTemplateWorkingFolder
        {
            get { return GetValue("PassTemplateWorkingFolder"); }
        }
        #endregion

        #region IApplePassGeneratorConfig
        public string AppleWebServerUrl
        {
            get { return GetValue("AppleWebServerUrl"); }
        }
        public string AppleWWDRCAPath
        {
            get { return GetValue("AppleWWDRCAPath"); }
        }
        #endregion

        public string PassWorkingFolder
        {
            get { return GetValue("PassWorkingFolder"); }
        }
    }
}
