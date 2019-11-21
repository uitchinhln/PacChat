using CNetwork;
using CNetwork.Protocols;
using CNetwork.Sessions;
using DotNetty.Transport.Channels;
using PacChatServer.Entity;
using PacChatServer.Entity.Meta.Profile;
using PacChatServer.Network.Packets.Login;
using PacChatServer.Network.Packets.Register;
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

        public ChatUser Owner { get; set; }

        public ChatSession(PacChatServer server, IChannel channel, ProtocolProvider protocolProvider, IConnectionManager connectionManager) 
            : base(channel, protocolProvider.HandShake)
        {
            Server = server;
            this.protocolProvider = protocolProvider;
            this.connectionManager = connectionManager;
        }

        public void FinalizeLogin(ChatUserProfile profile)
        {
            Owner = ChatUserManager.LoadUser(profile.ID);

            ChatUserManager.MakeOnline(Owner);

            Owner.LastLogon = DateTime.Now;
            Owner.sessions.Add(this);

            Protocol = protocolProvider.MainProtocol;

            //Notify
            Server.Logger.Info(String.Format("User {0} has logged in at {1}", Owner.Email, getAddress()));
        }

        public override void Disconnect()
        {
            base.Disconnect();
        }

        private void UpdatePipeline(String key, IChannelHandler channelHandler)
        {
            Channel.Pipeline.Replace(key, key, channelHandler);
        }
    }
}
