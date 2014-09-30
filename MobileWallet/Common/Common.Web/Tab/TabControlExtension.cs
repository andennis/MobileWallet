using System.Web.Mvc;

namespace Common.Web.Tab
{
    public static class TabControlExtension
    {
        public static TabStripBuilder TabStrip(this HtmlHelper html)
        {
            return new TabStripBuilder();
        }

    }
}
