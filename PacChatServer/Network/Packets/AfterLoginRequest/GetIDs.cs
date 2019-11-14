using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChatServer.Network.Protocol;
using PacChatServer.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLoginRequest
{
    public class GetIDs : IPacket
    {
        public void Decode(IByteBuffer buffer)
        {
            //throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;  
            if (chatSession.Owner != null && chatSession.Protocol is AfterLoginProtocol)
            {
                GetIDsResult packet = new GetIDsResult();
                packet.ids = MySQLSto.Instance.GetAllUserIds();
                chatSession.Send(packet);
            }
        }
    }
}
