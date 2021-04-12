using System.Web.Mvc;
using System.Web.Routing;

namespace HockeyTeam
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PlayersCreate",
                url: "Players/Create",
                defaults: new { controller = "Players", action = "Create" }
            );

            routes.MapRoute(
                name: "PlayersbyPositionbyPage",
                url: "Players/{category}/Page{page}",
                defaults: new { controller = "Players", action = "Index" }
            );

            routes.MapRoute(
                name: "PlayersbyPage",
                url: "Players/Page{page}",
                defaults: new
                { controller = "Players", action = "Index" }
            );

            routes.MapRoute(
                name: "PlayersbyPosition",
                url: "Players/{position}",
                defaults: new { controller = "Players", action = "Index" }
            );

            routes.MapRoute(
                name: "PlayersIndex",
                url: "Players",
                defaults: new { controller = "Players", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
