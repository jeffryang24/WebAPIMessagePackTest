using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiWithMsgPack.MediaFormatter;

namespace WebApiWithMsgPack
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Support MessagePack
            config.Formatters.Add(new MessagePackMediaTypeFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
