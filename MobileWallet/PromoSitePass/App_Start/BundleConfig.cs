using System.Web;
using System.Web.Optimization;

namespace PromoSitePass
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/passDesigner").Include(
                       "~/Scripts/createCard/jquery-2.1.1.js",
                       "~/Scripts/createCard/bootstrap.js",
                       "~/Scripts/createCard/jquery.flippy.js",
                       "~/Scripts/createCard/mColorPicker.js",
                       "~/Scripts/createCard/jquery.simple-dtpicker.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/createCard/jquery-2.1.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/passDesignerCss").Include(
                      "~/Content/createCard/bootstrap-theme.css",
                      "~/Content/bootstrap.css",
                      "~/Content/createCard/skeleton.css",
                      "~/Content/createCard/style.css",
                      "~/Content/createCard/jquery.simple-dtpicker.css",
                      "~/Content/createCard/createCard.css"));
        }
    }
}
