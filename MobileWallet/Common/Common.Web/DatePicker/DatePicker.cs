using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI;

namespace Common.Web.DatePicker
{
    public class DatePicker : WidgetBase
    {
        public const string EventChange = "change";
        public const string EventOpen = "open";
        public const string EventClose = "close";

        private MonthTemplate _monthTemplate;
        private PopupAnimation _animation;

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
        public DateTime Min { get; set; }
        public DateTime Max { get; set; }
        public bool Enabled { get; set; }

        protected override void WriteInitializationScript(TextWriter writer)
        {
            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            base.WriteHtml(writer);
        }

    }
}
