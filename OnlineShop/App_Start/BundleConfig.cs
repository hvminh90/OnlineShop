using System.Web;
using System.Web.Optimization;

namespace OnlineShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            

            bundles.Add(new ScriptBundle("~/js/core").Include(
                      "~/assets/client/js/jquery-1.12.1.min.js",
                      "~/assets/client/js/jquery-ui.js",
                      "~/assets/client/js/bootstrap.min.js",
                      "~/assets/client/js/move-top.js",
                      "~/assets/client/js/easing.js"));


            bundles.Add(new ScriptBundle("~/js/common").Include(
                      "~/assets/client/js/controller/common.js"));


            bundles.Add(new StyleBundle("~/css/core").Include(
                      "~/assets/client/css/bootstrap.css",
                      "~/assets/client/css/bootstrap-theme.css",
                      "~/assets/client/css/bootstrap-social.css",
                      "~/assets/client/css/jquery-ui.css",
                      "~/assets/client/css/style.css",
                      "~/assets/client/css/font-awesome.min.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
