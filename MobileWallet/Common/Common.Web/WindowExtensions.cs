﻿using System.Web.Mvc;
using Common.Web.Controls.Popup;

namespace Common.Web
{
    public static class WindowExtensions
    {
        public static WindowBuilder PopupDialog(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Widget().Window();
        }
    }
}
