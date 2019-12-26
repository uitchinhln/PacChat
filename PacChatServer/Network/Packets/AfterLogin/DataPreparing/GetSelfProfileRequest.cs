using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChatServer.Entity;
using PacChatServer.IO.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.DataPreparing
{
    public class GetSelfProfileRequest : IPacket
    {
        public void Decode(IByteBuffer buffer)
        {
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            ChatUser user = new ChatUserStore().Load(chatSession.Owner.ID);
            GetSelfProfileResponse packet = new GetSelfProfileResponse();
            packet.FirstName = user.FirstName;
            packet.LastName = user.LastName;
            packet.Email = user.Email;
            packet.Town = user.Town;
            packet.DateOfBirth = user.DateOfBirth;
            packet.Gender = user.Gender;

            chatSession.Send(packet);
        }
    }
}
