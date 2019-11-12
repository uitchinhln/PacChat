using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entities;
using PacChatServer.Entities.Properties;
using PacChatServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.Register
{
    public class RegisterRequest : IPacket
    {
        public string Email { get; set; }
        public string PassHashed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public Gender Gender { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Email = ByteBufUtils.ReadUTF8(buffer);
            PassHashed = ByteBufUtils.ReadUTF8(buffer);
            FirstName = ByteBufUtils.ReadUTF8(buffer);
            LastName = ByteBufUtils.ReadUTF8(buffer);
            DoB = new DateTime(buffer.ReadLong());
            Gender = (Gender)buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;
            PassHashed = HashUtils.MD5(PassHashed);

            User user = new User(-1);
            user.Email = Email;
            user.PassHashed = PassHashed;
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.DoB = DoB;
            user.Gender = Gender;

            chatSession.RegisterNewAccount(user);
        }
    }
}
