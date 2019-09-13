using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using PacChatServer.Net.Pipeline;
using PacChatServer.Net.Protocol;
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

        public ChatServer(PacChatServer server, ProtocolProvider protocolProvider) : base(server, protocolProvider)
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

                return channel;
            } catch (Exception e)
            {
                OnBindFailure(address, e);
            }

            return channel;
        }

        public override void OnBindFailure(IPEndPoint address, Exception t)
        {
            //Log there
        }

        public override async Task ShutdownAsync()
        {
            await channel.CloseAsync();
            Task.WaitAll(bossGroup.ShutdownGracefullyAsync(), workerGroup.ShutdownGracefullyAsync());
        }
    }
}
