using BlogFall.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Areas.Admin.Controllers
{
    [Breadcrumb("Anasayfa")]
    public class DashboardController : AdminBaseController
    {
  
        [Breadcrumb("İndeks")]
        public ActionResult Index()
        {
            return View();
        }
    }
}