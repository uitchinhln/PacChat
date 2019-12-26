using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.DataPreparing
{
    public class DisplayedProfileRequest : IPacket
    {
        public Guid TargetID { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            TargetID = Guid.Parse(ByteBufUtils.ReadUTF8(buffer));
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;
            ChatUser targetUser = ChatUserManager.LoadUser(TargetID);

            if (targetUser == null) return;

            DisplayedProfileResponse packet = new DisplayedProfileResponse();
            packet.ID = targetUser.ID.ToString();
            packet.Name = targetUser.FirstName + " " + targetUser.LastName;
            packet.Email = targetUser.Email;
            packet.DoB = targetUser.DateOfBirth.ToString();
            packet.Town = targetUser.Town;

            chatSession.Send(packet);
        }
    }
}
