using PacChatServer.Network.Packets.AfterLogin.DataPreparing;
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
            Inbound(0x00, new FriendsListRequest());
            Outbound(0x00, new FriendsListResponse());
        }
    }
}
