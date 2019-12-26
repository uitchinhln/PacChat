using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace PacChatServer.Network.RestAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Adding to the pipeline with our own middleware
            app.Use(async (context, next) =>
            {
                // Add Header
                context.Response.Headers["Product"] = "PacChat RestAPI";

                // Call next middleware
                await next.Invoke();
            });

            app.Use<CustomMiddleware>();

            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

            config.Services.Replace(typeof(IExceptionHandler), new PacChatExceptionHandler());

            // Web Api
            app.UseWebApi(config);
        }
    }

    
}
