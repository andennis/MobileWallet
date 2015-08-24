using System.Collections.Generic;

namespace Common.Web.Controls.ColorPicker
{
    public class ColorPickerEventBuilder : EventBuilder
    {
        public ColorPickerEventBuilder(IDictionary<string, string> events)
            :base(events)
        {
        }

        public ColorPickerEventBuilder Change(string handler)
        {
            AddHandler(ColorPicker.EventChange, handler);
            return this;
        }
        public ColorPickerEventBuilder Select(string handler)
        {
            AddHandler(ColorPicker.EventSelect, handler);
            return this;
        }
        public ColorPickerEventBuilder Open(string handler)
        {
            AddHandler(ColorPicker.EventOpen, handler);
            return this;
        }
        public ColorPickerEventBuilder Close(string handler)
        {
            AddHandler(ColorPicker.EventClose, handler);
            return this;
        }

    }
}
