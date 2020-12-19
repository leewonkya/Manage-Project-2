using System.Web;
using System.Web.Optimization;

namespace Project2.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/css/All").Include(
                        "~/Content/asset/vendors/bootstrap-4.5.2-dist/bootstrap.css",
                        "~/Content/asset/vendors/fontawesome-free-5.14.0-web/all.min.css",
                        "~/Content/asset/vendors/sweet-alert/sweet-alert.css",
                        "~/Content/style.css"
                ));
            bundles.Add(new ScriptBundle("~/css/jqueryUI").Include(
                        "~/Content/asset/vendors/jqueryUI/jquery-ui.min.css",
                        "~/Content/asset/vendors/select2/select2.min.css"
                ));
            bundles.Add(new ScriptBundle("~/js/jqueryUI").Include(
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/select2.min.js"
                ));
            bundles.Add(new ScriptBundle("~/js/All").Include(
                        "~/Scripts/jquery-3.5.1.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/all.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/sweet-alert.min.js"
                ));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
