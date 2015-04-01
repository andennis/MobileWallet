using System.IO;
using System.Web.UI;

namespace Common.Web.DatePicker
{
    public class DatePicker : DatePickerBase
    {
        private MonthTemplate _monthTemplate = new MonthTemplate();

        protected override void WriteInitializationScript(TextWriter writer)
        {
            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            base.WriteHtml(writer);
        }
        public string ARIATemplate { get; set; }
        public MonthTemplate MonthTemplate { get { return _monthTemplate; } }
        public bool EnableFooter { get; set; }
        public string Footer { get; set; }
        public string FooterId { get; set; }
        public string Start { get; set; }
        public string Depth { get; set; }

    }
}
