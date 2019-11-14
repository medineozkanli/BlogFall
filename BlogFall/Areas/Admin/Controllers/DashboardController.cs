using BlogFall.Areas.Admin.ViewModel;
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
            DashboardIndexViewModel model = new DashboardIndexViewModel
            {
                CategoryCount = db.Categories.Count(),
                PostCount = db.Posts.Count(),
                UserCount = db.Users.Count(),
                CommentCount = db.Comments.Count()
            };
            return View(model);
        }
    }
}