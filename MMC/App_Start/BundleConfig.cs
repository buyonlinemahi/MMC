using System.Web;
using System.Web.Optimization;

namespace MMC
{

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        , "~/Scripts/jquery-ui-1.11.4.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*"
                //,"~/Scripts/jquery.validate*"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/alertify").Include(
                        "~/Scripts/alertify/alertify.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/css/boot*"
                        , "~/Content/css/jquery.validate.bootstrap.css"
                        , "~/Content/css/main.css"
                        , "~/Content/css/alertify/alertify.core.css"
                        , "~/Content/css/alertify/alertify.default.css"
                        , "~/Content/css/datepicker.min.css"
                        ,"~/Content/css/jquery-ui.css"
                        , "~/Content/css/jquery.tagsinput.css"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/jquery.form.js"
                         , "~/Scripts/bootstrap.js"
                         , "~/Scripts/moment.min.js"
                         , "~/Scripts/datepicker/bootstrap-datepicker.min.js"
                         , "~/Scripts/knockout*"                       
                         , "~/Scripts/jquery.form.js"
                         , "~/Scripts/knockout.custom-binding-handlers/knockout.custom-binding-handlers.js"
                         , "~/Scripts/jquery.maskedinput.js"
                         , "~/Scripts/Custom/main.js"
                         , "~/Scripts/jquery.tagsinput.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/MMCScripts").Include(
                "~/Scripts/AjaxUtil.js"

               ));

        }
    }
}