#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

#endregion

namespace Evolent_ContactsManager
{
    /// <summary>
    /// WebApiConfiguration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register WebApi
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

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
