﻿using BlogFall.Areas.Admin.ViewModel;
using BlogFall.Models;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using BlogFall.Attributes;
using BlogFall.Utility;

namespace BlogFall.Areas.Admin.Controllers
{
    [Breadcrumb("Yazılar")]
    public class PostsController : AdminBaseController//miras almazsak dbyi kullanamayız.
    {
        [Breadcrumb("Anasayfa")]
        public ActionResult Index()
        {
            return View(db.Posts.OrderByDescending(x => x.CreationTime).ToList());
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
        [Breadcrumb("Düzenle")]
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");
            PostEditViewModel vm = db.Posts.Select(x => new PostEditViewModel
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Content = x.Content,
                Title = x.Title,
                Slug = x.Slug
            }).FirstOrDefault(x => x.Id == id);
            return View(vm);
        }


        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Breadcrumb("Düzenle")]
        public ActionResult Edit(PostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = db.Posts.Find(model.Id);
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;
                post.Title = model.Title;
                post.Slug = model.Slug;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            return View();

        }
        [Breadcrumb("Yeni")]
        public ActionResult New()
        {

            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "CategoryName");

            return View("Edit", new PostEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Breadcrumb("Yeni")]
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
                    CreationTime = DateTime.Now,
                    Slug = model.Slug
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
            if (file == null || file.ContentLength == 0 || !file.ContentType.StartsWith("image/"))//yüklenen dosya resim olmalı başı aynı sonu jpg png gibi değişir.
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
        [HttpPost]
        public ActionResult GenerateSlug(string title)
        {
            return Json(UrlService.URLFriendly(title));
        }
    }
}