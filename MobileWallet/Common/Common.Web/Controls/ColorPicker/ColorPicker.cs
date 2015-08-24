using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;

namespace Common.Web.Controls.ColorPicker
{
    public class ColorPicker : WidgetBase
    {
        public const string EventChange = "change";
        public const string EventSelect = "select";
        public const string EventOpen = "open";
        public const string EventClose = "close";

        public ColorPicker(ViewContext viewContext)
            :base(viewContext)
        {
        }

        protected override void WriteInitializationScript(TextWriter writer)
        {
            
        }
        protected override void WriteHtml(HtmlTextWriter writer)
        {
            
        }

        public ColorPickerPalette Palette { get; set; }
        public IEnumerable<string> PaletteColors { get; set; }
        public string ToolIcon { get; set; }
        public string Value { get; set; }
        public bool Enabled { get; set; }
        public bool Opacity { get; set; }
        public bool Buttons { get; set; }
        public object TileSize { get; set; }

    }
}
