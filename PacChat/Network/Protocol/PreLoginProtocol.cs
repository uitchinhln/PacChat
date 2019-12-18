using CNetwork;
using CNetwork.Sessions;
using PacChat.Network.Packets;
using PacChat.Network.Packets.Login;
using PacChat.Network.Packets.Ping;
using PacChat.Network.Packets.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Protocol
{
    public class PreLoginProtocol : PacChatProtocol
    {
        public PreLoginProtocol() : base("PreLogin")
        {
            Inbound(0x0, new PingRespone());
            Outbound(0x0, new Ping4Send());

            Inbound(0x02, new LoginResult());
            Outbound(0x02, new LoginData());

            Inbound(0x03, new RegisterResult());
            Outbound(0x03, new RegisterData());

            Inbound(0x04, new ReconnectResponse());
            Outbound(0x04, new ReconnectResquest());
        }
    }
}
