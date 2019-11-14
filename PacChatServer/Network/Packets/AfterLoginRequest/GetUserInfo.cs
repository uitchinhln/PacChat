using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChatServer.Entities;
using PacChatServer.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLoginRequest
{
    public class GetUserInfo : IPacket
    {
        public int id;

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;              
            GetUserInfoResult packet = new GetUserInfoResult();
            packet.user = MySQLSto.Instance.GetUser(id);
            chatSession.Send(packet);   
        }
    }
}
