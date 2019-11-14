using PacChatServer.Network.Packets.AfterLoginRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Protocol
{
    public class AfterLoginProtocol : PacChatProtocol
    {
        public AfterLoginProtocol() : base("After Login")
        {
            Inbound(0x00, new GetIDs());
            Outbound(0x00, new GetIDsResult());
        }
    }
}
