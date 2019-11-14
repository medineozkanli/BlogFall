using BlogFall.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Areas.Admin.Controllers
{
    [Breadcrumb("Kullanıcılar")]
    public class UsersController : AdminBaseController //miras aldık
    {
        // GET: Admin/Users
        [Breadcrumb("İndeks")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        public ActionResult ChangeStatus(string UserId, bool isEnabled)
        {
            var user = db.Users.Find(UserId);
            user.IsEnabled = isEnabled;
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}