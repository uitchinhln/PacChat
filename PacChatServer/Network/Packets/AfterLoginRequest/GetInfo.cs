using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entities;
using PacChatServer.Network.Protocol;
using PacChatServer.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLoginRequest
{
    public class GetInfo : IPacket
    {
        public int Id { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Id = Int32.Parse(ByteBufUtils.ReadUTF8(buffer));
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
                GetInfoResult packet = new GetInfoResult();
                User user = MySQLSto.Instance.GetUser(Id);
                packet.FirstName = user.FirstName;
                packet.LastName = user.LastName;
                packet.Email = user.Email;
                packet.Gender = (int)user.Gender;
                chatSession.Send(packet);
            }
        }
    }
}
