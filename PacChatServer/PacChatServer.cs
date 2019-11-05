using CNetwork;
using DotNetty.Transport.Channels;
using log4net;
using PacChatServer.Command;
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
        static PacChatServer instance = null;

        public IPAddress IP { get; set; }
        public int Port { get; set; }

        public ILog Logger { get; } = LogManager.GetLogger("Main");

        private ProtocolProvider protocolProvider;

        public SessionRegistry SessionRegistry { get; }

        public PacChatServer()
        {
            instance = this;

            protocolProvider = new ProtocolProvider();
            SessionRegistry = new SessionRegistry();

            _ = CommandManager.Instance;

            Start();
        }

        private void Start()
        {
            CountdownLatch latch = new CountdownLatch(1);

            ChatServer server = new ChatServer(this, protocolProvider, latch);
            _ = server.Bind(new IPEndPoint(IPAddress.Parse("10.90.104.145"), 1402));

            latch.Wait();

            new ConsoleReader();
        }

        public static CommandManager GetCommandManager()
        {
            return CommandManager.Instance;
        }

        public static PacChatServer GetServer()
        {
            return instance;
        }
    }
}
