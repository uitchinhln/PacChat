﻿using CNetwork;
using DotNetty.Transport.Channels;
using log4net;
using PacChatServer.Command;
using PacChatServer.Command.Commands;
using PacChatServer.Command.Commands.Ban;
using PacChatServer.Entity;
using PacChatServer.Entity.Meta.Profile;
using PacChatServer.IO.Storage;
using PacChatServer.MessageCore.Sticker;
using PacChatServer.Network;
using PacChatServer.Network.Protocol;
using PacChatServer.Network.RestAPI;
using PacChatServer.Utils;
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
        public RestServer RestAPI { get; private set; }

        public ILog Logger { get; } = LogManager.GetLogger("Main");

        private ProtocolProvider protocolProvider;

        public SessionRegistry SessionRegistry { get; }

        public PacChatServer()
        {
            instance = this;
            new ServerSettings();

            protocolProvider = new ProtocolProvider();
            SessionRegistry = new SessionRegistry();

            Mongo.StartService();
            ProfileCache.StartService();
            CommandManager.StartService();

            RegisterCommand();

            Sticker.StartService();

            StartNetworkService();
        }

        private void StartNetworkService()
        {
            CountdownLatch latch = new CountdownLatch(1);

            this.Network = new ChatServer(this, protocolProvider, latch);
            _ = this.Network.Bind(new IPEndPoint(ServerSettings.SERVER_HOST, ServerSettings.SERVER_PORT));

            RestAPI = new RestServer();
            RestAPI.Start(ServerSettings.WEBSERVER_HOST, ServerSettings.WEBSERVER_PORT, latch);

            latch.Wait();

            new ConsoleManager();
        }

        public void RegisterCommand()
        {
            GetCommandManager().RegisterCommand("sample", new SampleCommand());
            GetCommandManager().RegisterCommand("ban", new BanCommand());
            GetCommandManager().RegisterCommand("unban", new UnbanCommand());
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
