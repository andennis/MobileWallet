using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Container.Core.Entities.Template
{
    public class Template
    {
        [JsonProperty(PropertyName = "templateName", Required = Required.Always)]
        public string TemplateName { get; set; }

        [JsonProperty(PropertyName = "templateDescription")]
        public string TemplateDescription { get; set; }

        //Information that is required for all passes
        #region Standard Keys

        [JsonProperty(PropertyName = "passDescription", Required = Required.Always)]
        public string PassDescription { get; set; }

        [JsonProperty(PropertyName = "formatVersion", Required = Required.Always)]
        public int FormatVersion { get; set; }

        [JsonProperty(PropertyName = "organizationName", Required = Required.Always)]
        public string OrganizationName { get; set; }

        [JsonProperty(PropertyName = "passTypeIdentifier", Required = Required.Always)]
        public string PassTypeIdentifier { get; set; }

        [JsonProperty(PropertyName = "serialNumber", Required = Required.Always)]
        public string SerialNumber { get; set; }

        [JsonProperty(PropertyName = "teamIdentifier", Required = Required.Always)]
        public string TeamIdentifier { get; set; }

        #endregion
    }
}
