using PacChatServer.Network.Packets;
using PacChatServer.Network.Packets.Login;
using PacChatServer.Network.Packets.Ping;
using PacChatServer.Network.Packets.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Protocol
{
    public class HandShakeProtocol : PacChatProtocol
    {
        public HandShakeProtocol() : base("HandShake")
        {
            Inbound(0x00, new PingReceive());
            Outbound(0x00, new PingRespone());

            Inbound(0x02, new LoginRequest());
            Outbound(0x02, new LoginResult());

            Inbound(0x03, new RegisterRequest());
            Outbound(0x03, new RegisterResult());

            Inbound(0x04, new ReconnectResquest());
            Outbound(0x04, new ReconnectResponse());
        }
    }
}
