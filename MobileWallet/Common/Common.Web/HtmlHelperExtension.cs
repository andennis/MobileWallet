using System.Web.Mvc;

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
