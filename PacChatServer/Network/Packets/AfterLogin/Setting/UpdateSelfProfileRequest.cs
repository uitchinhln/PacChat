using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity;
using PacChatServer.Entity.EntityProperty;
using PacChatServer.IO.Entity;
using PacChatServer.Network.Packets.AfterLogin.DataPreparing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Setting
{
    public class UpdateSelfProfileRequest : IPacket
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Town { get; set; } = "Default";
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            FirstName = ByteBufUtils.ReadUTF8(buffer);
            LastName = ByteBufUtils.ReadUTF8(buffer);
            Town = ByteBufUtils.ReadUTF8(buffer);
            DateOfBirth = DateTime.Parse(ByteBufUtils.ReadUTF8(buffer));
            Gender = (Gender)buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            
            //if info is validated
            if (true)
            {
                ChatUser user = chatSession.Owner;
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.Town = Town;
                user.DateOfBirth = DateOfBirth;
                user.Gender = Gender;
                user.Save();

                GetSelfProfileResponse packet = new GetSelfProfileResponse();
                packet.FirstName = user.FirstName;
                packet.LastName = user.LastName;
                packet.Email = user.Email;
                packet.Town = user.Town;
                packet.DateOfBirth = user.DateOfBirth;
                packet.Gender = user.Gender;
                chatSession.Owner.Send(packet);
            }
        }
    }
}
