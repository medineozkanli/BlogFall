using BlogFall.Areas.Admin.ViewModel;
using BlogFall.Models;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;

namespace BlogFall.Areas.Admin.Controllers
{
    public class PostsController : AdminBaseController//miras almazsak dbyi kullanamayız.
    {
        // GET: Admin/Posts
        public ActionResult Index()
        {
            return View(db.Posts.OrderByDescending(x =>x.CreationTime).ToList());
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");
            PostEditViewModel vm = db.Posts.Select(x => new PostEditViewModel
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Content = x.Content,
                Title = x.Title
            }).FirstOrDefault(x => x.Id == id);
            return View(vm);
        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = db.Posts.Find(model.Id);
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;
                post.Title = model.Title;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            return View();
        }
        public ActionResult New()
        {

            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            return View("Edit", new PostEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult New(PostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    AuthorId = User.Identity.GetUserId(),
                    CreationTime = DateTime.Now
                };
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            return View("Edit", new PostEditViewModel());
        }
        [HttpPost]
        public ActionResult AjaxImageUpload(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength ==0 || !file.ContentType.StartsWith("image/"))//yüklenen dosya resim olmalı başı aynı sonu jpg png gibi değişir.
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //diğer durumda dosya yükleme.
            var saveFolderPath = Server.MapPath("~/Upload/Posts");//Harddiskte fiziksel olarak klasörün bulunduğu yolu döndürür.(Kaydederken lazım)
            var ext = Path.GetExtension(file.FileName);//.jpg dosya ismi (uzantısı) alıyo
            var saveFileName = Guid.NewGuid().ToString() + ext;
            var saveFilePath = Path.Combine(saveFolderPath, saveFileName);//Bu isimle kaydet birleştirip.
            file.SaveAs(saveFilePath);//kaydediyor.

            return Json(new { url = Url.Content("~/Upload/Posts/" + saveFileName) });
        }
    }
}