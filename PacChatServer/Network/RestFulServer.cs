using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using PacChatServer.Network.Pipeline;
using PacChatServer.Network.Protocol;
using PacChatServer.Utils.ThreadUtils;

namespace PacChatServer.Network
{
    public class RestFulServer : ChatNetworkServer
    {
        protected IEventLoopGroup bossGroup;
        protected IEventLoopGroup workerGroup;
        protected ServerBootstrap bootstrap;
        protected IChannel Channel { get; set; }

        public RestFulServer(PacChatServer server, ProtocolProvider protocolProvider, CountdownLatch latch) : base(server, protocolProvider, latch)
        {
            this.bossGroup = new MultithreadEventLoopGroup(1);
            this.workerGroup = new MultithreadEventLoopGroup();
            this.bootstrap = new ServerBootstrap();

            this.bootstrap
                .Group(bossGroup, workerGroup)
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.SoBacklog, 1024 * 1024 * 25)
                .ChildHandler(new RestFulServerChannelInitializer());
        }

        public override async Task Bind(IPEndPoint address)
        {
            try
            {
                Channel = await this.bootstrap.BindAsync(address.Port);
                OnBindSuccess(address);
            }
            catch (Exception e)
            {
                OnBindFailure(address, e);
            }
        }

        public override void OnBindSuccess(IPEndPoint address)
        {
            base.OnBindSuccess(address);
            Server.IP = address.Address;
            Server.Port = address.Port;
            Server.Logger.Info("Bind success. FileServer is listening on " + Server.IP + ":" + Server.Port);
        }

        public override void OnBindFailure(IPEndPoint address, Exception e)
        {
            Server.Logger.Error("Bind Failured", e);
        }

        public override void Shutdown()
        {
            Channel.CloseAsync();
            bootstrap.Group().ShutdownGracefullyAsync();
            bootstrap.ChildGroup().ShutdownGracefullyAsync();
        }
    }
}
