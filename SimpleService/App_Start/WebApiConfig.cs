﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace SimpleService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Formatting JSON
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
