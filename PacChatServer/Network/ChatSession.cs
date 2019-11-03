using CNetwork;
using CNetwork.Protocols;
using CNetwork.Sessions;
using DotNetty.Transport.Channels;
using PacChatServer.Entities;
using PacChatServer.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network
{
    public class ChatSession : BasicSession
    {
        public PacChatServer Server { get; }

        ProtocolProvider protocolProvider; 

        IConnectionManager connectionManager;

        public User Owner { get; set; }

        public ChatSession(PacChatServer server, IChannel channel, ProtocolProvider protocolProvider, IConnectionManager connectionManager) 
            : base(channel, protocolProvider.HandShake)
        {
            Server = server;
            this.protocolProvider = protocolProvider;
            this.connectionManager = connectionManager;
        }

        private void FinalizeLogin()
        {

        }

        public override void Disconnect()
        {
            base.Disconnect();
        }

        private void UpdatePipiline(String key, IChannelHandler channelHandler)
        {
            Channel.Pipeline.Replace(key, key, channelHandler);
        }
    }
}
