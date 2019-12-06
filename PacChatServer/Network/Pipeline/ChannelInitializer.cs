using CNetwork;
using CNetwork.Pipeline;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Codecs;
using DotNetty.Codecs.Protobuf;
using DotNetty.Handlers.Logging;

namespace PacChatServer.Network.Pipeline
{
    class ChannelInitializer : BasicChannelInitializer
    {
        /**
         * The time in seconds which are elapsed before a client is disconnected due to a read timeout.
         */
        private static int READ_IDLE_TIMEOUT = 20;

        /**
         * The time in seconds which are elapsed before a client is deemed idle due to a write timeout.
         */
        private static int WRITE_IDLE_TIMEOUT = 15;

        private ChatServer connectionManager;

        public ChannelInitializer(ChatServer connectionManager) : base(connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        protected override void InitChannel(ISocketChannel channel)
        {
            IChannelPipeline pipeline = channel.Pipeline;

            pipeline.AddLast(new LoggingHandler("SRV-CONN"));
            pipeline.AddLast("framing-enc", new ProtobufVarint32LengthFieldPrepender());
            pipeline.AddLast("framing-dec", new ProtobufVarint32FrameDecoder());

            channel.Pipeline
                .AddLast("idle_timeout", new IdleStateHandler(READ_IDLE_TIMEOUT, WRITE_IDLE_TIMEOUT, 0));
            base.InitChannel(channel);
        }
    }
}
