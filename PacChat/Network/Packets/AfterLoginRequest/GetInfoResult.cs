using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChat.ChatAMVC;
using PacChat.ChatPageContents.ViewModels;
using PacChat.MVC;
using PacChat.Windows.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.Network.Packets.AfterLoginRequest
{
    public class GetInfoResult : IPacket
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            int firstNameLength = buffer.ReadInt();
            int lastNameLength = buffer.ReadInt();
            int emailLength = buffer.ReadInt();

            FirstName = buffer.ReadString(firstNameLength, Encoding.UTF8);
            LastName = buffer.ReadString(lastNameLength, Encoding.UTF8);
            Email = buffer.ReadString(emailLength, Encoding.UTF8);

            Gender = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            Console.WriteLine(FirstName + LastName + "/" + Email + "/" + Gender);
        }
    }
}
