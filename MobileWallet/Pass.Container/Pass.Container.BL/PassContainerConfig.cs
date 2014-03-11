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

        #region IPassContainerConfig
        public string PassTemplateFolderName
        {
            get { return GetValue("PassTemplateFolderName"); }
        }
        public string PassTemplateFileName
        {
            get { return GetValue("PassTemplateFileName"); }
        }
        public string PassGeneratorTempFolderPath
        {
            get { return GetValue("PassGeneratorTempFolderPath"); }
        }
        #endregion

        #region IDbConfig
        public string ConnectionString
        {
            get { return GetValue("ConnectionStringName"); }
        }
        #endregion

        #region IPassDistributionConfig
        public string SecurityKey
        {
            get { return GetValue("SecurityKey"); }
        }
        #endregion

    }
}
