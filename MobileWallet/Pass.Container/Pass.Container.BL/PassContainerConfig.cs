using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Configuration;
using Pass.Container.Core;


namespace Pass.Container.BL
{
    public sealed class PassContainerConfig : AppConfigBase, IPassContainerConfig
    {
        public string PassTemplateFolderName
        {
            get { return GetValue("PassTemplateFolderName"); }
        }
        public string PassTemplateFileName
        {
            get { return GetValue("PassTemplateFileName"); }
        }
    }
}
