using System.Web.Mvc;
using Common.Web.Controls.DatePicker;
using Common.Web.Popup;

namespace Common.Web
{
    public class WidgetFactory
    {
        private readonly HtmlHelper _htmlHelper;

        public WidgetFactory(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public virtual WindowBuilder Window()
        {
            return new WindowBuilder(new Window(_htmlHelper.ViewContext));
        }

        public virtual DatePickerBuilder DatePicker()
        {
            return new DatePickerBuilder(new DatePicker(_htmlHelper.ViewContext));
        }

    }
}
