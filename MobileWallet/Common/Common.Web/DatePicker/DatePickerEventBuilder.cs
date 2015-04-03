using System.Collections.Generic;

namespace Common.Web.DatePicker
{
    public class DatePickerEventBuilder : EventBuilder
    {
        public DatePickerEventBuilder(IDictionary<string, string> events)
            :base(events)
        {
        }

        public DatePickerEventBuilder Change(string handler)
        {
            AddHandler(DatePicker.EventChange, handler);
            return this;
        }
        public DatePickerEventBuilder Open(string handler)
        {
            AddHandler(DatePicker.EventOpen, handler);
            return this;
        }
        public DatePickerEventBuilder Close(string handler)
        {
            AddHandler(DatePicker.EventClose, handler);
            return this;
        }

    }
}
