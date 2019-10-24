using CNetwork;
using DotNetty.Transport.Channels;
using log4net;
using PacChatServer.Network;
using PacChatServer.Network.Protocol;
using PacChatServer.Utils.ThreadUtils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PacChatServer
{
    public class PacChatServer
    {
        public IPAddress IP { get; set; }
        public int Port { get; set; }

        public ILog Logger { get; } = LogManager.GetLogger("Main");

        private ProtocolProvider protocolProvider;

        public SessionRegistry SessionRegistry { get; }

        public PacChatServer()
        {
            protocolProvider = new ProtocolProvider();
            SessionRegistry = new SessionRegistry();

            Start();
        }

        private void Start()
        {
            CountdownLatch latch = new CountdownLatch(1);

            ChatServer server = new ChatServer(this, protocolProvider, latch);
            server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1402));

            latch.Wait();

            new ConsoleReader();
        }
    }
}
