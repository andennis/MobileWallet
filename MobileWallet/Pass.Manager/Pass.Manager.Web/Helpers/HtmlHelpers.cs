using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Pass.Manager.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString PassDesigner(this HtmlHelper helper)
        {
            var builder = new StringBuilder();
            builder.Append(helper.Partial("_PassDesigner"));
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString PassSettingsTab(this HtmlHelper helper)
        {
            var builder = new StringBuilder();
            builder.Append(helper.Partial("_PassSettingsTab"));
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString DesignTab(this HtmlHelper helper)
        {
            var builder = new StringBuilder();
            builder.Append(helper.Partial("_DesignTab"));
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString FrontContentTab(this HtmlHelper helper)
        {
            var builder = new StringBuilder();
            builder.Append(helper.Partial("_FrontContentTab"));
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString BackContentTab(this HtmlHelper helper)
        {
            var builder = new StringBuilder();
            builder.Append(helper.Partial("_BackContentTab"));
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString LockScreenTab(this HtmlHelper helper)
        {
            var builder = new StringBuilder();
            builder.Append(helper.Partial("_LockScreenTab"));
            return new MvcHtmlString(builder.ToString());
        }
        public static MvcHtmlString LanguagesTab(this HtmlHelper helper)
        {
            var builder = new StringBuilder();
            builder.Append(helper.Partial("_LanguagesTab"));
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString DistributionTab(this HtmlHelper helper)
        {
            var builder = new StringBuilder();
            builder.Append(helper.Partial("_DistributionTab"));
            return new MvcHtmlString(builder.ToString());
        }
    }
}