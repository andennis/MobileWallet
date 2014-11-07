﻿using System.Web.Mvc;

namespace Common.Web
{
    public static class HtmlHelperExtension
    {
        private readonly static WidgetFactory _widgetFactory = new WidgetFactory();

        public static WidgetFactory Kendo(this HtmlHelper helper)
        {
            return _widgetFactory;
        }
    }
}
