using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Areas.Admin.Controllers
{
    public class CategoriesController : AdminBaseController
    {
        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.Categories.OrderByDescending(x => x.CategoryName).ToList());
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            if (category.Posts.Count >0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "bir şeyler");
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}