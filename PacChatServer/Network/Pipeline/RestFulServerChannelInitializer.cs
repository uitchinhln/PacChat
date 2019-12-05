using DotNetty.Codecs.Http;
using DotNetty.Transport.Channels;
using PacChatServer.Network.FileServer.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Pipeline
{
    public class RestFulServerChannelInitializer : ChannelInitializer<IChannel>
    {
        protected override void InitChannel(IChannel channel)
        {
            channel.Pipeline
                .AddLast("encoder", new HttpResponseEncoder())
                .AddLast("decoder", new HttpRequestDecoder(4096, 8192, 8192, false))
                .AddLast("aggregator", new HttpObjectAggregator(8388608)) // 8MB
                .AddLast("handler", new RestFulServerHandler());
        }
    }
}
