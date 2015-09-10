using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Common.Extensions;

namespace Common.Web.Controls.ColorPicker
{
    public class ColorPicker : WidgetBase
    {
        public const string EventChange = "change";
        public const string EventSelect = "select";
        public const string EventOpen = "open";
        public const string EventClose = "close";

        public ColorPicker(ViewContext viewContext)
            : base(viewContext)
        {
        }

        protected override void WriteInitializationScript(TextWriter writer)
        {
            var settings = new ColorPickerSettings
                           {
                               toolIcon = ToolIcon,
                               value = Value,
                               enabled = Enabled,
                               opacity = Opacity,
                               buttons = Buttons,
                               tileSize = TileSize
                           };
            string jsonSettings = settings.ObjectToJson();
            string widgetScript = string.Format("$(\"#{0}\").kendoColorPicker({1})", Name, jsonSettings);
            string script = GetDocumentReadyScript(widgetScript).Replace("\"[", "[").Replace("]\"", "]");
            writer.WriteLine(script);
        }
        protected override void WriteHtml(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, Name);
            if (string.IsNullOrEmpty(Value))
            {

                var val = GetModelValue();
                if (val != null)
                {
                    Color color = Color.FromArgb(Int32.Parse(val.ToString()));
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B));
                }
            }
            foreach (KeyValuePair<string, object> attr in HtmlAttributes)
            {
                writer.AddAttribute(attr.Key, Convert.ToString(attr.Value));
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        public ColorPickerPalette Palette { get; set; }
        public IEnumerable<string> PaletteColors { get; set; }
        public string ToolIcon { get; set; }
        public string Value { get; set; }
        public bool Enabled { get; set; }
        public bool Opacity { get; set; }
        public bool Buttons { get; set; }
        public object TileSize { get; set; }

        public class ColorPickerSettings
        {
            public string toolIcon;
            public string value;
            public bool enabled;
            public bool opacity;
            public bool buttons;
            public object tileSize;
        }
    }
}
