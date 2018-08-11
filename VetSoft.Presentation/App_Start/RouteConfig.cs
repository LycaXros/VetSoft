using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VetSoft.Presentation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.f
            routes.MapPageRoute(
                "ReporteCequeo",
                "Chequeo/Reporte/{ChequeoID}",
                "~/Reportes/ReporteCertificadoVacunacion",
                true, null,
                new RouteValueDictionary { { "outgoing", new AspxRouteConstraint() } }
                );
            routes.MapMvcAttributeRoutes();

            //routes.MapPageRoute("", "Chequeo/Reporte/{ChequeoID}", "~/Reportes/ReporteCertificadoVacunacion");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
