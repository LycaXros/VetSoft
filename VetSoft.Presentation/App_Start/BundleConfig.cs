using System.Web;
using System.Web.Optimization;

namespace VetSoft.Presentation
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/allScripts").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/jquery.validate*",
                    "~/Scripts/jquery-ui.min.js",
                    "~/Scripts/notify.min.js",
                    "~/Scripts/datatables.js",
                    "~/Scripts/jquery.mask.min.js",
                    "~/Scripts/select2.min.js",
                    "~/Scripts/otf.js"

                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery-ui.min.css",
                      "~/Content/select2.min.css",
                      "~/Content/PagedList.css",
                      "~/Content/DataTable.css",
                      "~/Content/site.css"));
        }
    }
}
