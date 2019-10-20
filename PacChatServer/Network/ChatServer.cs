﻿using CNetwork;
using CNetwork.Sessions;
using DotNetty.Transport.Channels;
using PacChatServer.Network.Pipeline;
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
    public class ChatServer : ChatSocketServer, IConnectionManager
    {
        public ChatServer(PacChatServer server, ProtocolProvider protocolProvider, CountdownLatch latch) : base(server, protocolProvider, latch)
        {
            this.bootstrap.ChildHandler(new ChannelInitializer(this));
        }

        public override void OnBindSuccess(IPEndPoint address)
        {
            base.OnBindSuccess(address);
            Server.IP = address.Address;
            Server.Port = address.Port;
            Server.Logger.Info("Bind success. Server is listening on " + Server.IP + ":" + Server.Port);
        }

        public override void OnBindFailure(IPEndPoint address, Exception e)
        {
            Server.Logger.Error("Bind Failured", e);
        }

        public ISession NewSession(IChannel c)
        {
            ChatSession session = new ChatSession(Server, c, protocolProvider, this);
            Server.SessionRegistry.Add(session);
            return session;
        }

        public void SessionInactivated(ISession session)
        {
            Server.SessionRegistry.Remove((ChatSession) session);
        }
    }
}