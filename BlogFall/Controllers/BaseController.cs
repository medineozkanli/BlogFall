using BlogFall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFall.Controllers
{
    public class BaseController : Controller// Bütün controllerda ne kullanılacaksa onları buraya ekle.
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)//controller çöpe atılırken
        {
            base.Dispose(disposing);
            if (disposing)
            {
                db.Dispose();//db nesnesinide çöpe atıyo burda.
            }
        }
    }
}