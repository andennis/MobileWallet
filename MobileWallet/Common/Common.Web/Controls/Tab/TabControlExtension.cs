﻿using System.Web.Mvc;

namespace Common.Web.Controls.Tab
{
    public static class TabControlExtension
    {
        public static TabStripBuilder TabStrip(this HtmlHelper html)
        {
            return new TabStripBuilder();
        }

    }
}
