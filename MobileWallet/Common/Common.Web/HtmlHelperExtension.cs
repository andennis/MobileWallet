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

        public static ActionInfo GetCurrentAction(this HtmlHelper helper)
        {
            return new ActionInfo()
            {
                Controller = helper.ViewContext.RouteData.Values["controller"] as string,
                Action = helper.ViewContext.RouteData.Values["action"] as string
            };
        }
    }
}
