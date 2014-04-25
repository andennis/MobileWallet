using Common.Configuration;
using Pass.Container.Core;

namespace Pass.Container.BL
{
    public sealed class PassContainerConfig : AppConfigBase, IPassContainerConfig
    {
        public PassContainerConfig()
            : base("PassContainer")
        {
        }

        #region IPassTemplateConfig
        public string PassTemplateFolderName
        {
            get { return GetValue("PassTemplateFolderName"); }
        }
        public string PassTemplateFileName
        {
            get { return GetValue("PassTemplateFileName"); }
        }
        public string ApplePassTemplateWebServerUrl
        {
            get { return GetValue("ApplePassTemplateWebServerUrl"); }
        }
        #endregion

        #region IDbConfig
        public string ConnectionString
        {
            get { return GetValue("ConnectionStringName"); }
        }
        #endregion

        #region IPassGeneratorConfig
        public string PassGeneratorTempFolderPath
        {
            get { return GetValue("PassGeneratorTempFolderPath"); }
        }
        #endregion

        #region IApplePassGeneratorConfig
        public string AppleWWDRCAPath
        {
            get { return GetValue("AppleWWDRCAPath"); }
        }
        #endregion
    }
}
