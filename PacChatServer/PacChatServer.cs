﻿using CNetwork;
using DotNetty.Transport.Channels;
using log4net;
using PacChatServer.Command;
using PacChatServer.Command.Commands;
using PacChatServer.Entity;
using PacChatServer.Entity.Meta.Profile;
using PacChatServer.IO.Storage;
using PacChatServer.Network;
using PacChatServer.Network.Protocol;
using PacChatServer.Utils.ThreadUtils;
using System;
using System.Collections.Concurrent;
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

            Mongo.StartService();
            ProfileCache.StartService();

            CommandManager.StartService();

            GetCommandManager().RegisterCommand("sample", new SampleCommand());

            StartNetworkService();
        }

        private void StartNetworkService()
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
