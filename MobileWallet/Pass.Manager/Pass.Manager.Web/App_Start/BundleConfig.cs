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
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/DataTables-1.10.2/css/jquery.dataTables.css",
                      "~/Content/common.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/DataTables-1.10.2/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                "~/Scripts/moment.js"/*,
                "~/Scripts/moment-with-locales.js"*/));

            //Kendo
            bundles.Add(new StyleBundle("~/content/kendo").Include(
                "~/Content/kendo/2014.2.716/kendo.common-bootstrap.min.css",
                "~/Content/kendo/2014.2.716/kendo.bootstrap.min.css",
                //"~/Content/kendo/2014.2.716/kendo.common.min.css",
                "~/Content/kendo/2014.2.716/kendo.default.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/2014.2.716/kendo.core.min.js",
                //Culture scripts
                "~/Scripts/kendo/2014.2.716/cultures/kendo.culture.ru-RU.min.js",
                "~/Scripts/kendo/2014.2.716/cultures/kendo.culture.en-US.min.js",
                "~/Scripts/kendo/2014.2.716/cultures/kendo.culture.en-GB.min.js",

                //TabStrip
                "~/Scripts/kendo/2014.2.716/kendo.data.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.tabstrip.min.js",

                //Window
                "~/Scripts/kendo/2014.2.716/kendo.userevents.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.draganddrop.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.window.min.js",

                //DatePicker
                "~/Scripts/kendo/2014.2.716/kendo.calendar.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.popup.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.datepicker.min.js",
                
                //DropDownList
                "~/Scripts/kendo/2014.2.716/kendo.list.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.fx.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.userevents.min.js",
                //"~/Scripts/kendo/2014.2.716/kendo.draganddrop.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.mobile.scroller.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.dropdownlist.min.js",
                
                //ColorPicker
                //"~/Scripts/kendo/2014.2.716/kendo.color.min.js", ???
                "~/Scripts/kendo/2014.2.716/kendo.slider.min.js",
                "~/Scripts/kendo/2014.2.716/kendo.colorpicker.min.js",

                //panelbar
                "~/Scripts/kendo/2014.2.716/kendo.panelbar.min.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/jquery.form.js",
                "~/Scripts/FormAutoFill/jquery.formautofill.js",
                "~/Scripts/Grid.js",
                "~/Scripts/Action.js",
                "~/Scripts/PopupWindow.js",
                "~/Scripts/AjaxSetup.js"
                ));


            //Control extensions
            bundles.Add(new StyleBundle("~/content/ctrlext").Include(
                "~/Content/fileUploadHelper.css"));

            bundles.Add(new ScriptBundle("~/bundles/ctrlext").Include(
                "~/Scripts/fileUploadHelper.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
