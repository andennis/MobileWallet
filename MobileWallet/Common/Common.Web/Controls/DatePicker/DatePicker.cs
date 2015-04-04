using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Common.Extensions;
using Common.Extensions.JsonNetConverters;
using Newtonsoft.Json;

namespace Common.Web.Controls.DatePicker
{
    public class DatePicker : WidgetBase
    {
        public const string EventChange = "change";
        public const string EventOpen = "open";
        public const string EventClose = "close";

        private MonthTemplate _monthTemplate;
        private PopupAnimation _animation;
        private DateTime? _value;

        public DatePicker(ViewContext viewContext)
            :base(viewContext)
        {
        }

        public string ARIATemplate { get; set; }
        public MonthTemplate MonthTemplate { get { return _monthTemplate ?? (_monthTemplate = new MonthTemplate()); } }
        public bool EnableFooter { get; set; }
        public string Footer { get; set; }
        public string FooterId { get; set; }
        public string Start { get; set; }
        public string Depth { get; set; }

        public PopupAnimation Animation { get { return _animation ?? (_animation = new PopupAnimation()); } }
        public string Culture { get; set; }
        public CultureInfo CultureInfo { get; set; }
        public IList<DateTime> Dates { get; set; }
        public string Format { get; set; }
        public List<string> ParseFormats { get; set; }
        public DateTime? Value { get; set; }
        public DateTime? Min { get; set; }
        public DateTime? Max { get; set; }
        public bool Enabled { get; set; }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, Name);
            if (!this.Value.HasValue)
            {
                var val = (DateTime?)GetModelValue();
                if (val.HasValue)
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, val.Value.ToString(this.Format ?? "d"));
            }
            foreach (KeyValuePair<string, object> attr in HtmlAttributes)
            {
                writer.AddAttribute(attr.Key, Convert.ToString(attr.Value));
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        private class DatePickerSettings
        {
            public string ARIATemplate;
            public string culture;
            public string depth;
            public string footer;
            public string format;
            public DateTime? max;
            public DateTime? min;
            public MonthTemplate month;
            public IList<string> parseFormats;
            public string start;

            [JsonConverter(typeof(DateTimeToJavaScriptDateConverter))]
            public DateTime? value;

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string change;

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string close;

            [JsonConverter(typeof(JsonValueWithoutQuotesConverter))]
            public string open;
        }

        protected override void WriteInitializationScript(TextWriter writer)
        {
            var settings = new DatePickerSettings
                           {
                               ARIATemplate = ARIATemplate,
                               culture = Culture,
                               depth = Depth,
                               footer = Footer,
                               format = Format,
                               max = Max,
                               min = Min,
                               month = (_monthTemplate != null
                                        ? new MonthTemplate { Content = _monthTemplate.Content, Empty = _monthTemplate.Empty } 
                                        : null),
                               parseFormats = ParseFormats,
                               start = Start,
                               value = Value,
                               change = GetEvent(EventChange),
                               close = GetEvent(EventClose),
                               open = GetEvent(EventOpen)
                           };

            string jsonSettings = settings.ObjectToJson();
            string widgetScript = string.Format("$(\"#{0}\").kendoDatePicker({1})", Name, jsonSettings);
            string script = GetDocumentReadyScript(widgetScript);
            writer.WriteLine(script);

        }

    }
}
