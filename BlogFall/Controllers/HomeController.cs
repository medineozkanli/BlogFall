using BlogFall.Models;
using BlogFall.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Controllers
{
    public class HomeController : BaseController// Burda baseden miras aldık 
    {

        public ActionResult Index(int? cid, int page = 1)
        {
            int pageSize = 5;//Bir sayfada gözükecek yazı sayısı
            ViewBag.SubTitle = "Yazılarım";
            IQueryable<Post> result = db.Posts;//sorgulanabilir liste fitreleme.
            Category cat = null;

            if (cid != null)
            {
                cat = db.Categories.Find(cid);
                if (cat == null)
                {
                    return HttpNotFound();
                }
                ViewBag.SubTitle = cat.CategoryName;
                result = result.Where(x => x.CategoryId == cid);//Bu sefer filtrelediğimizi yolladık.
            }
            ViewBag.page = page;
            ViewBag.pageCount = Math.Ceiling(result.Count() / (decimal)pageSize);
            ViewBag.nextPage = page + 1;
            ViewBag.prievPage = page - 1;
            ViewBag.cid = cid;
            return View(result.OrderByDescending(x => x.CreationTime).Skip((page - 1) * pageSize).Take(pageSize).ToList());//skip atla take  tane al demek orderby olmadAN ÇALIŞMAZ.
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CategoriesPartial()
        {
            return PartialView("_CategoriesPartial", db.Categories.ToList());
        }

        public ActionResult ShowPost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        public ActionResult SendComment(SendCommentViewModel model)
        {
            if (ModelState.IsValid)//hatasız geldiyse
            {
                Comment comment = new Comment
                {
                    AuthorId = User.Identity.GetUserId(),
                    AuthorName = model.AuthorName,
                    AuthorEmail = model.AuthorEmail,
                    Content = model.Content,
                    CreationTime = DateTime.Now,
                    ParenId = model.ParentId,
                    PostId = model.PostId
                };
                db.Comments.Add(comment);
                db.SaveChanges();
                return Json(comment);//yorum eklemek için.Oluşmuş commentin bilgileri ajax metodu ile kullanıcıya geri döndürdük.
            }
            var errorList = ModelState.Values.SelectMany(m => m.Errors)//model stateteki hata mesajlarını bir liste olarak yolluyor.
                .Select(e => e.ErrorMessage)
                .ToList();
            return Json(new { Errors = errorList });
        }
    }
}