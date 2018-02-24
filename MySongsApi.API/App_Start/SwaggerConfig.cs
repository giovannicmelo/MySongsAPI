using System.Web.Http;
using WebActivatorEx;
using MySongsApi.API;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MySongsApi.API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "MySongs API");
                        c.IncludeXmlComments(GetXmlCommentsPath());
                        c.PrettyPrint();
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("Documentation of MySongs API");
                    });
        }

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\MySongsApi.API.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
