using CNetwork.Utils;
using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Transport.Channels;
using PacChatServer.Network.RestfulServer.Alias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.RestfulServer.Handler
{
    public class RestFulServerHandler : SimpleChannelInboundHandler<IFullHttpRequest>
    {
        protected override void ChannelRead0(IChannelHandlerContext ctx, IFullHttpRequest msg)
        {
            IFullHttpResponse response = new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.NotFound, Unpooled.Buffer(), false);

            AliasManager.Instance.ExecuteAlias(msg.Uri, msg, response);

            ctx.WriteAndFlushAsync(response);
            ctx.CloseAsync();
        }
    }
}
