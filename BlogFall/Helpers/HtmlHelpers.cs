using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Helpers
{
    public static class HtmlHelpers
    {
        public static string ControllerName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
            
        }
        public static string ActionName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["action"].ToString();

        }
    }
}