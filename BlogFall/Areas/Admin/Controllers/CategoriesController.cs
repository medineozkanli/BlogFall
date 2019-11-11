using BlogFall.Attributes;
using BlogFall.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Areas.Admin.Controllers
{
    [Breadcrumb("Kategoriler")]
    public class CategoriesController : AdminBaseController
    {
        [Breadcrumb("İndeks")]
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

            if (category.Posts.Count > 0)
            {
                return Json(new { success = false, error = "Silmek istediğiniz kategoride yazılar bulunduğundan silinememektedir." });
            }

            db.Categories.Remove(category);
            db.SaveChanges();
            return Json(new { success = true });
        }
        [Breadcrumb("Yeni")]
        public ActionResult New()
        {
            return View("Edit", new Category());//id alanı zorunlu hata vermesin diye boş bir kategori döndürdük.
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Breadcrumb("Yeni")]
        public ActionResult New(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(model);
                db.SaveChanges();
                TempData["successMessage"] = "Yeni kategori başarıyla eklendi";//Tek kullanımlık.
                return RedirectToAction("Index");
            }
            return View("Edit", model);//modeli döndürdük çünkü kategori eklemi düzenle mi anlasın.
        }
        [Breadcrumb("Düzenle")]
        public ActionResult Edit(int id)
        {
            return View("Edit", db.Categories.Find(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Breadcrumb("Düzenle")]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                TempData["successMessage"] = "Kategori ismi başarı ile değiştirildi";//Tek kullanımlık.//Actionlar arası veri taşıma
                return RedirectToAction("Index");
            }
            return View("Edit", model);
        }



    }
}