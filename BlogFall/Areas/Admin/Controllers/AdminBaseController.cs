using BlogFall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]//Rolünde admin olanlar ve miras alanlar bu controllera girebilir.Sıradan kayıt olmuş biri giremez.
    public abstract class AdminBaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}