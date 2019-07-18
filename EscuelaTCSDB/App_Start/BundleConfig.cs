using System.Web;
using System.Web.Optimization;

namespace EscuelaTCSDB
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/Tooltips/adapter-jquery.js",
                "~/Scripts/Tooltips/opentip.js",
                "~/Scripts/datatables/jquery.datatables.js",
                        "~/Scripts/datatables/datatables.bootstrap.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-tourist.js"));

            //Bundle tooltips js 
            bundles.Add(new ScriptBundle("~/Content/js/tooltip").Include(
                      "~/Content/tooltipster/tooltipster-master/dist/js/tooltipster.bundle.min.js"));

            //bundle tooltips css
            bundles.Add(new StyleBundle("~/Content/css/tooltip").Include(
                      "~/Content/tooltipster/tooltipster-master/dist/css/tooltipster.bundle.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                "~/Scripts/toastr.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-tourist.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"));
            
        }
    }
}
