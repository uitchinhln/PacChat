using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Codecs.Http;

namespace PacChatServer.Network.FileServer.Alias.AliasHandler
{
    public class PageNotFound : IAliasExecutor
    {
        public void Execute(IFullHttpRequest request, IFullHttpResponse response)
        {
            throw new NotImplementedException();
        }
    }
}
