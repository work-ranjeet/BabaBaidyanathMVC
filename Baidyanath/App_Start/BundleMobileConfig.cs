using System.Web;
using System.Web.Optimization;

namespace Baidyanath {
    public class BundleMobileConfig {
        public static void RegisterBundles(BundleCollection bundles) {

            bundles.Add(new ScriptBundle("~/bundles/jquerymobile").Include("~/Scripts/jquery.mobile*"));

            bundles.Add(new StyleBundle("~/Content/Mobile/css").Include("~/Content/Site.Mobile.css"));

            bundles.Add(new StyleBundle("~/Content/mobilecss").Include("~/Content/jquery.mobile*"));

        }
    }
}