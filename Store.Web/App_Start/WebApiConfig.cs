using System.Web.Http;

namespace Store.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ShopApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PageApi",
                routeTemplate: "api/{controller}/{page}/{start}/{end}",
                defaults: new { page = "Page", id = RouteParameter.Optional }
            );
        }
    }
}
