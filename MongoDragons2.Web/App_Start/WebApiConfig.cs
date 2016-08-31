using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MongoDragons2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action=RouteParameter.Optional, id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.JsonFormatter;
            config.Formatters.Clear();
            config.Formatters.Add(jsonFormatter);
        }
    }
}
