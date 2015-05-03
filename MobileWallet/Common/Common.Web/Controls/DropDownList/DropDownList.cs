using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;

namespace Common.Web.Controls.DropDownList
{
    public class DropDownList : ListBase
    {
        public const string EventSelect = "select";
        public const string EventChange = "change";
        public const string EventDataBound = "dataBound";
        public const string EventOpen = "open";
        public const string EventClose = "close";
        public const string EventCascade = "cascade";

        private IList<DropDownListItem> _items;

        public DropDownList(ViewContext viewContext /*, ViewDataDictionary viewData*/)
            : base(viewContext)
        {
        }

        public IList<DropDownListItem> Items { get { return _items ?? (_items = new List<DropDownListItem>()); } } 

        public string Template { get; set; }
        public string TemplateId { get; set; }
        public string Value { get; set; }

        protected override void WriteInitializationScript(TextWriter writer)
        {
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            
        }
        public bool? AutoBind { get; set; }
        public string CascadeFrom { get; set; }
        public string CascadeFromField { get; set; }
        public string DataValueField { get; set; }
        public object OptionLabel { get; set; }
        public int? SelectedIndex { get; set; }
        public string Text { get; set; }
        public string ValueTemplate { get; set; }
        public string ValueTemplateId { get; set; }

    }
}
