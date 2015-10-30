using System.Web;
using System.Web.Optimization;

namespace WeChatService.Demo
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            //jweixin-1.0.0
            bundles.Add(new ScriptBundle("~/bundles/jweixin").Include(
                       "~/Scripts/jweixin-1.0.0.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-3.2.0.js",
                "~/Scripts/knockout.mapping-latest.js"));
            //Initialize
            bundles.Add(new ScriptBundle("~/bundles/initialize").Include(
               "~/Scripts/JS/Initialize.js"));

            //ScanQrCode
            bundles.Add(new ScriptBundle("~/bundles/ScanQrCode").Include(
                        "~/Scripts/JS/ScanQrCode.js"));
            //Photo
            bundles.Add(new ScriptBundle("~/bundles/Photo").Include(
                        "~/Scripts/JS/Photo.js"));
            //NetworkType
            bundles.Add(new ScriptBundle("~/bundles/NetworkType").Include(
                        "~/Scripts/JS/NetworkType.js"));
            //Location
            bundles.Add(new ScriptBundle("~/bundles/Location").Include(
                        "~/Scripts/JS/Location.js"));
            //Record
            bundles.Add(new ScriptBundle("~/bundles/Record").Include(
                        "~/Scripts/JS/Record.js"));
            //TranslateVoice
            bundles.Add(new ScriptBundle("~/bundles/TranslateVoice").Include(
                        "~/Scripts/JS/TranslateVoice.js"));
        }
    }
}
