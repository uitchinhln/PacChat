using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using static System.Net.Mime.MediaTypeNames;

namespace PacChatServer.Network.RestAPI
{
    public class CustomMiddleware : OwinMiddleware
    {
        public CustomMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            try
            {
                context.Response.Headers["MachineName"] = Environment.MachineName;

                await Next.Invoke(context);
            } catch (Exception e)
            {

            }
        }
    }

    public class PacChatExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new NotFoundResult(context.Request);
        }
    }
}
