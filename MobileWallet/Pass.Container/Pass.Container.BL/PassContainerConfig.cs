﻿using Common.Configuration;
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
        public string PassTemplateWorkingFolder
        {
            get { return GetValue("PassTemplateWorkingFolder"); }
        }
        #endregion

        #region IDbConfig
        public string ConnectionString
        {
            get { return GetValue("ConnectionStringName"); }
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
