using CNetwork;
using CNetwork.Sessions;
using PacChat.Network.Packets;
using PacChat.Network.Packets.Login;
using PacChat.Network.Packets.Ping;
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
        }
    }
}
