using System.Web;
using System.Web.Optimization;

namespace BlogFall
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;//Siteyi geliştirirken usecdn kullanır siteyi publish ederken de verilen dosyayı kullanır.Debug modda localini kullanıyosun ama yayınlarsan publish edersen internet sayfasındakini kullanıyosun.
            bundles.Add(new ScriptBundle("~/bundles/jquery", "https://code.jquery.com/jquery-3.4.1.min.js").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.css",
                      "~/Content/fontawesome-all.css",
                      "~/Content/Site.css"));

             #if DEBUG // derleyicide hangisini çalışıp hangisinin çalışmayacağını söylüyor.(web.configde var modumuz complication)
                        BundleTable.EnableOptimizations = false; //Bunu yazmazsak localdekini kullanıyor yazarsak cdn dekini kullandı.
            #else
                        BundleTable.EnableOptimizations = true;//bu yayınlanırken aktif olmalı
            #endif
        }
    }
}
