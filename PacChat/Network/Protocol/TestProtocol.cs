using CNetwork;
using CNetwork.Sessions;
using PacChat.Network.Packets;
using PacChat.Network.Packets.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Protocol
{
    public class TestProtocol : PacChatProtocol
    {
        public TestProtocol() : base("Test")
        {
            Inbound(0x0, new PingRespone());
            Outbound(0x0, new Ping4Send());
        }
    }
}
