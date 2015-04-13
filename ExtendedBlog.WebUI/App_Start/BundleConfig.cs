using System.Web;
using System.Web.Optimization;

namespace ExtendedBlog.WebUI
{
    public class BundleConfig
    {
        // Дополнительные сведения о Bundling см. по адресу http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                            .Include("~/Scripts/bootstrap*"));

            bundles.Add(new StyleBundle("~/Content/css")
                //.Include("~/Content/Site.css") //главное, не перепутать порядок!!
                .Include("~/Content/bootstrap*")
                .Include("~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/bundles/admin")
                            .Include("~/Content/bootstrap*")
                            .Include("~/Content/admin.css")
                            .Include("~/Scripts/jqGrid/css/ui.jqgrid.css"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //    "~/Scripts/jquery.unobtrusive*",
            //    "~/Scripts/jquery.validate*"));

            // jquery validation library bundle
            var jqueryValBundle = new ScriptBundle("~/jqueryval", "http://ajax.aspnetcdn.com/ajax/jquery.validate/1.10.0/jquery.validate.min.js")
                                        .Include("~/Scripts/jquery.validate.js");
            bundles.Add(jqueryValBundle);

            // jquery unobtrusive validation library
            var jqueryUnobtrusiveValBundle = new ScriptBundle("~/jqueryunobtrusiveval", "http://ajax.aspnetcdn.com/ajax/mvc/3.0/jquery.validate.unobtrusive.min.js")
                                                .Include("~/Scripts/jquery.validate.unobtrusive.js");
            bundles.Add(jqueryUnobtrusiveValBundle);

            bundles.Add(new ScriptBundle("~/bundles/jqGrid")
                            .Include("~/Scripts/jqGrid/i18n/grid.locale-ru.js")
                            .Include("~/Scripts/jqGrid/jquery.jqGrid*"));

            bundles.Add(new StyleBundle("~/Content/themes/simple/jqueryuicustom/css/sunny")
                            .Include("~/Content/themes/simple/jqueryuicustom/css/sunny/jquery-ui-1.9.2.custom.css"));

            // jQuery UI library bundle
            bundles.Add(new ScriptBundle("~/jqueryui", "http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.1/jquery-ui.min.js")
                                            .Include("~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/Scripts/tiny_mce/js")
                                            .Include("~/Scripts/tiny_mce/tiny_mce.js"));
        }
    }
}