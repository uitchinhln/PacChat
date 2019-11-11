using CNetwork;
using DotNetty.Transport.Channels;
using log4net;
using PacChatServer.Command;
using PacChatServer.Command.Commands;
using PacChatServer.Network;
using PacChatServer.Network.Protocol;
using PacChatServer.Storage;
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

        public ChatServer Network { get; private set; }

        public ILog Logger { get; } = LogManager.GetLogger("Main");

        private ProtocolProvider protocolProvider;

        public SessionRegistry SessionRegistry { get; }

        public PacChatServer()
        {
            instance = this;

            protocolProvider = new ProtocolProvider();
            SessionRegistry = new SessionRegistry();

            _ = CommandManager.Instance;
            _ = MySQLSto.Instance;

            GetCommandManager().RegisterCommand("sample", new SampleCommand());

            Start();
        }

        private void Start()
        {
            CountdownLatch latch = new CountdownLatch(1);

            this.Network = new ChatServer(this, protocolProvider, latch);
            _ = this.Network.Bind(new IPEndPoint(ServerSettings.SERVER_HOST, ServerSettings.SERVER_PORT));

            latch.Wait();

            new ConsoleManager();
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
