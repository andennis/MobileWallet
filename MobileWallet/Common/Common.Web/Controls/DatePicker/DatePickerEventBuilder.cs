using System.Collections.Generic;

namespace Common.Web.Controls.DatePicker
{
    public class DatePickerEventBuilder : EventBuilder
    {
        public DatePickerEventBuilder(IDictionary<string, string> events)
            :base(events)
        {
        }

        public DatePickerEventBuilder Change(string handler)
        {
            AddHandler(Controls.DatePicker.DatePicker.EventChange, handler);
            return this;
        }
        public DatePickerEventBuilder Open(string handler)
        {
            AddHandler(Controls.DatePicker.DatePicker.EventOpen, handler);
            return this;
        }
        public DatePickerEventBuilder Close(string handler)
        {
            AddHandler(Controls.DatePicker.DatePicker.EventClose, handler);
            return this;
        }

    }
}
