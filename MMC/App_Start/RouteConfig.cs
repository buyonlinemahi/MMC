using System.Web.Mvc;
using System.Web.Routing;

namespace MMC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
             name: "ClinicalAuditAction",
             url: "PreParation/ClinicalAuditAction/{id}/{ProcessLevel}",
             defaults: new { controller = "PreParation", action = "ClinicalTriageAction" });
            routes.MapRoute(
            name: "ClinicalTriageAction",
            url: "PreParation/ClinicalTriageAction/{id}/{ProcessLevel}",
            defaults: new { controller = "PreParation", action = "ClinicalTriageAction" });
     
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default1",
                url: "{controller}/{action}/{id}/{id2}/{id3}",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional, id2 = UrlParameter.Optional,id3=UrlParameter.Optional }
            );
        }
    }
}