using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using DotNetty.Codecs.Http;

namespace PacChatServer.Network.RestfulServer.Alias.AliasHandler
{
    public class PageNotFound : IAliasExecutor
    {
        public void Execute(IFullHttpRequest request, IFullHttpResponse response)
        {
            response.SetStatus(HttpResponseStatus.NotFound);
            response.Replace(Unpooled.Empty);
        }
    }
}
