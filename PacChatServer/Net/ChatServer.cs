using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using log4net;
using PacChatServer.Net.Pipeline;
using PacChatServer.Net.Protocol;
using PacChatServer.Utils.ThreadUtils;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PacChatServer.Net
{
    class ChatServer : PacChatNetworkServer
    {
        IEventLoopGroup bossGroup;
        IEventLoopGroup workerGroup;
        ServerBootstrap bootstrap;

        IChannel channel;

        public ChatServer(PacChatServer server, ProtocolProvider protocolProvider, CountdownLatch latch) 
            : base(server, protocolProvider, latch, typeof(ChatServer))
        {
            bossGroup = new MultithreadEventLoopGroup(1);
            workerGroup = new MultithreadEventLoopGroup();

            bootstrap = new ServerBootstrap();
            bootstrap
                .Group(bossGroup, workerGroup)
                .Channel<TcpServerSocketChannel>()
                .ChildOption(ChannelOption.TcpNodelay, true)
                .ChildOption(ChannelOption.SoKeepalive, true)
                .ChildHandler(new ServerChannelInitializer(this));
        }

        public override async Task<IChannel> BindAsync(IPEndPoint address)
        {
            try
            {
                channel = await bootstrap.BindAsync(address.Address, address.Port);
                OnBindSuccess();
            } catch (Exception e)
            {
                OnBindFailure(address, e);
            }

            return channel;
        }

        public override void OnBindFailure(IPEndPoint address, Exception t)
        {
            logger.Error("Cannot bind to " + address.ToString(), t);
        }

        public override async Task ShutdownAsync()
        {
            await channel.CloseAsync();
            Task.WaitAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
        }
    }
}
