using CNetwork;
using CNetwork.Protocols;
using CNetwork.Sessions;
using DotNetty.Transport.Channels;
using PacChat.Network.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network
{
    public class ClientSession : BasicSession
    {
        ProtocolProvider protocolProvider;
        public ClientSession(IChannel channel, ProtocolProvider protocolProvider) : base(channel, protocolProvider.Test)
        {
            this.protocolProvider = protocolProvider;
        }

        public override void Send(IPacket packet)
        {
            base.Send(packet);
        }

        public void LoggedIn()
        {
            Protocol = protocolProvider.MainProtocol;
        }
    }
}
