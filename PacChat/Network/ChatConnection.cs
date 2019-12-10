using CNetwork;
using CNetwork.Sessions;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using PacChat.MVC;
using PacChat.Network.Pipeline;
using PacChat.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network
{
    public class ChatConnection : IConnectionManager
    {
        public ClientSession Session { get; private set; }
        static ChatConnection instance;
        protected IEventLoopGroup workerGroup;
        protected Bootstrap bootstrap;
        protected IChannel Channel;
        protected ProtocolProvider protocolProvider;

        private ChatConnection(ProtocolProvider protocolProvider)
        {
            this.protocolProvider = protocolProvider;
            this.bootstrap = new Bootstrap();
            this.workerGroup = new MultithreadEventLoopGroup();

            bootstrap
                .Group(workerGroup)
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ChannelInitializer(this));
        }

        public async Task Bind()
        {
            string ip = ConfigurationManager.AppSettings["ServerAddress"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["ServerPort"]);
            IPAddress address = IPAddress.Parse(ip);
            await Bind(new IPEndPoint(address, port));
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

        public IPAddress GetIPAddress()
        {
            if (Channel == null) return null;
            return (Channel.RemoteAddress as IPEndPoint).Address;
        }

        public bool IsConnected()
        {
            return Channel != null && Channel.Active;
        }

        public ISession NewSession(IChannel c)
        {
            Session = new ClientSession(c, protocolProvider);
            return Session;
        }

        public void SessionInactivated(ISession session)
        {
            Console.WriteLine("Server has disconnected!!!");
            AppManager.OnDisconnection(lostConnection:true);
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

        public async Task Send(IPacket packet)
        {
            try
            {
                if (Session == null || !IsConnected())
                {
                    await Bind();
                    AppManager.OnReconnected();
                }
                if (Session == null || !IsConnected())
                {
                    throw new ProtocolViolationException("Cannot connect to server");
                }
                Session.Send(packet);
            } catch 
            {
                throw;
            }
        }

        public static ChatConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChatConnection(new ProtocolProvider());
                }
                return instance;
            }
        }
    }
}
