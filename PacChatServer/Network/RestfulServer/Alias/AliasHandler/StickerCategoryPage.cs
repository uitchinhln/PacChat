using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Codecs.Http;

namespace PacChatServer.Network.RestfulServer.Alias.AliasHandler
{
    public class StickerCategoryPage : IAliasExecutor
    {
        public void Execute(IFullHttpRequest request, IFullHttpResponse response)
        {
            response.Content.WriteString("Hello", Encoding.UTF8);
        }
    }
}
