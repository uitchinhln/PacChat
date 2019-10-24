using CNetwork;
using CNetwork.Sessions;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using PacChat.Network.Pipeline;
using PacChat.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network
{
    public class NetworkClient : IConnectionManager
    {
        protected IEventLoopGroup workerGroup;
        protected Bootstrap bootstrap;
        protected IChannel Channel;
        protected ProtocolProvider protocolProvider;

        public NetworkClient(ProtocolProvider protocolProvider)
        {
            this.protocolProvider = protocolProvider;
            this.bootstrap = new Bootstrap();
            this.workerGroup = new MultithreadEventLoopGroup();

            bootstrap
                .Group(workerGroup)
                .Channel<TcpSocketChannel>()
                .Handler(new ChannelInitializer(this));
        }

        public async Task Bind(IPEndPoint address)
        {
            try
            {
                Channel = await this.bootstrap.ConnectAsync(address);
                OnBindSuccess(address);
            }
            catch (Exception e)
            {
                OnBindFailure(address, e);
            }
        }

        public ISession NewSession(IChannel c)
        {
            Session session = new Session(c, protocolProvider.Test);
            return session;
        }

        public void SessionInactivated(ISession session)
        {
            Console.WriteLine("Server is shutdown");
        }

        public void Shutdown()
        {
            Channel.CloseAsync();
            bootstrap.Group().ShutdownGracefullyAsync();
        }
        public void OnBindSuccess(IPEndPoint address)
        {
            Console.WriteLine("Connected to " + address);
        }

        public void OnBindFailure(IPEndPoint address, Exception e)
        {
        }
    }
}
