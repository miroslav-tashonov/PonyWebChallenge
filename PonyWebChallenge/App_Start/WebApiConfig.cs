using System.Web.Http;

namespace PonyWebChallenge
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "pony-challenge/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
