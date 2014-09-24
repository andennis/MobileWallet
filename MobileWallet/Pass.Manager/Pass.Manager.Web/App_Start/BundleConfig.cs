using System.Web.Optimization;

namespace Pass.Manager.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/DataTables-1.10.2/css/jquery.dataTables.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables-1.10.2/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/Grid.js",
                "~/Scripts/Action.js"));


            bundles.Add(new ScriptBundle("~/bundles/passDesigner").Include(
                       "~/Scripts/createCard/jquery-1.8.3.js",
                       "~/Scripts/createCard/bootstrap.js",
                       "~/Scripts/createCard/jquery.flippy.js",
                       "~/Scripts/createCard/mColorPicker.js",
                       "~/Scripts/createCard/jquery.simple-dtpicker.js",
                       "~/Scripts/createCard/StackBlur.js"
                       ));
            bundles.Add(new StyleBundle("~/bundles/passDesignerCss").Include(
                      "~/Content/createCard/bootstrap-theme.css",
                      "~/Content/bootstrap.css",
                      "~/Content/createCard/skeleton.css",
                      "~/Content/createCard/style.css",
                      "~/Content/createCard/jquery.simple-dtpicker.css",
                      "~/Content/createCard/createCard.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
