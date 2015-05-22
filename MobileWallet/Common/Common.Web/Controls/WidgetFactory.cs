using System.Web.Mvc;
using Common.Web.Controls.DatePicker;
using Common.Web.Controls.DropDownList;
using Common.Web.Controls.Popup;

namespace Common.Web.Controls
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
            return new DatePickerBuilder(new DatePicker.DatePicker(_htmlHelper.ViewContext));
        }

        public virtual DropDownListBuilder DropDownList()
        {
            return new DropDownListBuilder(new DropDownList.DropDownList(_htmlHelper.ViewContext));
        }
    }
}
