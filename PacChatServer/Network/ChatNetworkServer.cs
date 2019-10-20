using PacChatServer.Network.Protocol;
using PacChatServer.Utils.ThreadUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network
{
    public abstract class ChatNetworkServer
    {
        protected PacChatServer Server { get; }
        protected ProtocolProvider protocolProvider { get; }

        protected CountdownLatch latch;         

        public ChatNetworkServer(PacChatServer server, ProtocolProvider protocolProvider, CountdownLatch latch)
        {
            this.Server = server;
            this.protocolProvider = protocolProvider;
            this.latch = latch;
        }

        public abstract Task Bind(IPEndPoint address);

        public virtual void OnBindSuccess(IPEndPoint address)
        {
            latch.Signal();
        }

        public abstract void OnBindFailure(IPEndPoint address, Exception e);

        public abstract void Shutdown();
    }
}
