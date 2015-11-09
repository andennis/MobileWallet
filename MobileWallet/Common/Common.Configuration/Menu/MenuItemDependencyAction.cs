using System;
using System.Xml.Serialization;

namespace Common.Configuration.Menu
{
    [Serializable]
    public class MenuItemDependencyAction
    {
        [XmlAttribute("action")]
        public string Action { get; set; }

        [XmlAttribute("controller")]
        public string Controller { get; set; }
    }
}
