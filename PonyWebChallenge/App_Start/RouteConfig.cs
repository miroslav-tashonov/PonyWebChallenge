using System.Web.Mvc;
using System.Web.Routing;

namespace PonyWebChallenge
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "MazeView", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Maze",
                url: "pony-challenge/Maze/{id}",
                defaults: new { controller = "Maze", action = "Post", id = UrlParameter.Optional }
            );
        }
    }
}
