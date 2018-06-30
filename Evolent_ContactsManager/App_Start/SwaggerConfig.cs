#region Namespaces

using Evolent_ContactsManager;
using Swashbuckle.Application;
using System;
using System.Web.Http;
using WebActivatorEx;

#endregion

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Evolent_ContactsManager
{
    /// <summary>
    /// Swagger Configuration
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Register Swagger
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "ContactsManager");
                    c.PrettyPrint();
                    c.IncludeXmlComments(GetXmlCommentsPath());
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("Evolent - Contacts Manager");
                });
        }

        /// <summary>
        /// This will return a path where Swagger XML Coments are generated
        /// </summary>
        /// <returns>string</returns>
        static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\SwaggerXml\ContactsManager.xml", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
