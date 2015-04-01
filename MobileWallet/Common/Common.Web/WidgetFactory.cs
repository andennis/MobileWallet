using Common.Web.DatePicker;
using Common.Web.Popup;

namespace Common.Web
{
    public class WidgetFactory
    {
        public virtual WindowBuilder Window()
        {
            return new WindowBuilder(new Window());
        }

        public virtual DatePickerBuilder DatePicker()
        {
            return new DatePickerBuilder(new DatePicker.DatePicker());
        }

    }
}
