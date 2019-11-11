using CNetwork;
using CNetwork.Protocols;
using CNetwork.Sessions;
using DotNetty.Transport.Channels;
using PacChatServer.Entities;
using PacChatServer.Network.Packets.Login;
using PacChatServer.Network.Protocol;
using PacChatServer.Storage;
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

        public void Login(String username, String passhash, bool forceLogin)
        {
            LoginResult respone = new LoginResult();

            User user = MySQLSto.Instance.GetUser(username);
            if (user == null)
            {
                respone.StatusCode = 404;
            } else if (user.PassHashed != passhash)
            {
                respone.StatusCode = 401;
            } else if (user.Banned)
            {
                respone.StatusCode = 403;
            } else
            {
                respone.StatusCode = 200;
            }
            Send(respone);            
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
