using System.Web.Http;
using WebActivatorEx;
using LocationSearchEngine;
using Swashbuckle.Application;
using System;
using LocationSearchEngine.Common;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace LocationSearchEngine
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
              .EnableSwagger(c =>
              {
                  c.SingleApiVersion("v1", "Location Search Engine");
                  c.IncludeXmlComments(string.Format(@"{0}\bin\LocationSearchEngine.xml",
                                       System.AppDomain.CurrentDomain.BaseDirectory));
                  c.SchemaId(x => x.FullName);
              })
              .EnableSwaggerUi();
        }
      
    }
}
