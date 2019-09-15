using DotNetty.Transport.Channels;
using log4net;
using PacChatServer.Net.Protocol;
using PacChatServer.Utils.ThreadUtils;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PacChatServer.Net
{
    abstract class PacChatNetworkServer
    {
        protected ILog logger;
        protected PacChatServer server;
        protected ProtocolProvider provider;
        protected CountdownLatch latch;

        public PacChatNetworkServer(PacChatServer server, ProtocolProvider protocolProvider, CountdownLatch latch, Type type)
        {
            this.server = server;
            this.provider = protocolProvider;
            this.latch = latch;
            this.logger = log4net.LogManager.GetLogger(type);
        }

        public abstract Task<IChannel> BindAsync(IPEndPoint address);

        public void OnBindSuccess()
        {
            logger.Debug("Server is listening");
            latch.Signal();
        }

        public abstract void OnBindFailure(IPEndPoint address, Exception t);

        public abstract Task ShutdownAsync();
    }
}
