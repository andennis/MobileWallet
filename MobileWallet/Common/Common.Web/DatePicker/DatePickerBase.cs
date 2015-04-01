using System;
using System.Collections.Generic;

namespace Common.Web.DatePicker
{
    public class DatePickerBase : WidgetBase
    {
        //public PopupAnimation Animation { get; }
        //public string Culture { get; set; }
        //public CultureInfo CultureInfo { get; }
        public IList<DateTime> Dates { get; set; }
        public string Format { get; set; }
        public List<string> ParseFormats { get; set; }
        public DateTime? Value { get; set; }
        public DateTime Min { get; set; }
        public DateTime Max { get; set; }
        public bool Enabled { get; set; }

    }
}
