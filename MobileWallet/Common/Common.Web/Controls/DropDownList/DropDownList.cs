using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Common.Extensions;
using Common.Extensions.JsonNetConverters;
using Newtonsoft.Json;

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

        public bool? AutoBind { get; set; }
        public string CascadeFrom { get; set; }
        public string CascadeFromField { get; set; }
        public string DataValueField { get; set; }
        public object OptionLabel { get; set; }
        public int? SelectedIndex { get; set; }
        public string Text { get; set; }
        public string ValueTemplate { get; set; }
        public string ValueTemplateId { get; set; }

        public class DropDownListSettings
        {
            public bool? autoBind;
            public string dataSource;
            public string cascadeFrom;
            public string cascadeFromField;
            public string dataTextField;
            public string dataValueField;
            public object optionLabel;
            public int? selectedIndex;
            public string valueTemplate;

            public string template;
            public string value;

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string select;

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string change;

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string dataBound;

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string open;

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string close;

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string cascade;
        }

        private string GetDataSource()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{{Text: '{0}', Value: {1}}}", Items[0].Text, Items[0].Value);
            for (int i = 1; i < Items.Count; i++)
            {
                sb.AppendFormat(",{{Text: '{0}', Value: {1}}}", Items[i].Text, Items[i].Value);
            }
            return string.Format("[{0}]", sb);
        }
        protected override void WriteInitializationScript(TextWriter writer)
        {
            var settings = new DropDownListSettings
            {
                autoBind = AutoBind,
                dataSource = GetDataSource(),
                cascadeFrom = CascadeFrom,
                cascadeFromField = CascadeFromField,
                dataTextField = DataTextField,
                dataValueField = DataValueField,
                optionLabel = OptionLabel,
                selectedIndex = SelectedIndex,
                valueTemplate = ValueTemplate,
                template = Template,
                value = Value,
                select = GetEvent(EventSelect),
                change = GetEvent(EventChange),
                dataBound = GetEvent(EventDataBound),
                open = GetEvent(EventOpen),
                close = GetEvent(EventClose),
                cascade = GetEvent(EventCascade)
            };

            string jsonSettings = settings.ObjectToJson();
            string widgetScript = string.Format("$(\"#{0}\").kendoDropDownList({1})", Name, jsonSettings);
            string script = GetDocumentReadyScript(widgetScript).Replace("\"[", "[").Replace("]\"", "]");
            writer.WriteLine(script);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, Name);
            foreach (KeyValuePair<string, object> attr in HtmlAttributes)
            {
                writer.AddAttribute(attr.Key, Convert.ToString(attr.Value));
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }
    }
}
