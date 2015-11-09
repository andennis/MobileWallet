using System;
using System.Xml.Serialization;

namespace Common.Configuration.Menu
{
    [Serializable]
    public class MenuItem
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("controller")]
        public string Controller { get; set; }

        [XmlAttribute("action")]
        public string Action { get; set; }

        [XmlArray(ElementName = "items", IsNullable = false)]
        [XmlArrayItem(ElementName = "item", IsNullable = false)]
        public MenuItem[] Items { get; set; }

        //[XmlArray(ElementName = "DependencyActions", IsNullable = false)]
        [XmlArrayItem(ElementName = "action", IsNullable = false)]
        public MenuItemDependencyAction[] DependencyActions { get; set; }
    }
}
