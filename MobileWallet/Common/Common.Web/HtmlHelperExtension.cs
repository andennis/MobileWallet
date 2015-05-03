using System.Web.Mvc;
using Common.Web.Controls;

namespace Common.Web
{
    public static class HtmlHelperExtension
    {
        public static WidgetFactory Widget(this HtmlHelper helper)
        {
            return new WidgetFactory(helper);
        }
    }
}
