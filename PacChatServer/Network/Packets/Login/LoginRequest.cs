using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.Login
{
    public class LoginRequest : IPacket
    {
        public string Username { get; set; }
        public string Passhash { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Username = ByteBufUtils.ReadUTF8(buffer);
            Passhash = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;
            chatSession.Login(Username, Passhash, false);
        }
    }
}
