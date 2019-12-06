using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using PacChatServer.Network.Protocol;
using PacChatServer.Utils.ThreadUtils;

namespace PacChatServer.Network
{
    public abstract class ChatSocketServer : ChatNetworkServer
    {
        protected IEventLoopGroup bossGroup;
        protected IEventLoopGroup workerGroup;
        protected ServerBootstrap bootstrap;
        protected IChannel Channel { get; set; }

        public ChatSocketServer(PacChatServer server, ProtocolProvider protocolProvider, CountdownLatch latch) : base(server, protocolProvider, latch)
        {
            this.bossGroup = new MultithreadEventLoopGroup(1);
            this.workerGroup = new MultithreadEventLoopGroup();
            this.bootstrap = new ServerBootstrap();

            this.bootstrap
                .Group(bossGroup, workerGroup)
                .Option(ChannelOption.SoBacklog, 100)
                .Handler(new LoggingHandler("SRV-LSTN"))
                .Channel<TcpServerSocketChannel>()
                .ChildOption(ChannelOption.TcpNodelay, true)
                .ChildOption(ChannelOption.SoKeepalive, true);
        }

        public override async Task Bind(IPEndPoint address)
        {
            try
            {
                Channel = await this.bootstrap.BindAsync(address.Port);
                OnBindSuccess(address);
            } catch (Exception e)
            {
                OnBindFailure(address, e);
            }
        }

        public override void Shutdown()
        {
            Channel.CloseAsync();
            bootstrap.Group().ShutdownGracefullyAsync();
            bootstrap.ChildGroup().ShutdownGracefullyAsync();
        }
    }
}
