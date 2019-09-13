using DotNetty.Transport.Channels;
using PacChatServer.Net.Protocol;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PacChatServer.Net
{
    abstract class PacChatNetworkServer
    {
        protected PacChatServer server;
        protected ProtocolProvider provider;

        public PacChatNetworkServer(PacChatServer server, ProtocolProvider protocolProvider)
        {
            this.server = server;
            this.provider = protocolProvider;
        }

        public abstract Task<IChannel> BindAsync(IPEndPoint address);

        public void OnBindSuccess()
        {
            Console.WriteLine("Bind OK");
        }

        public abstract void OnBindFailure(IPEndPoint address, Exception t);

        public abstract Task ShutdownAsync();
    }
}
