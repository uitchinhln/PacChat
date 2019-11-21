using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLoginRequest
{
    public class GetInfoResult : IPacket
    {
        public string Email { get; set; }
        public int[] len = new int[3];
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            len[0] = FirstName.Length;
            len[1] = LastName.Length;
            len[2] = Email.Length;

            foreach (var x in len) byteBuf.WriteInt(x);

            byteBuf.WriteString(FirstName, Encoding.UTF8);
            byteBuf.WriteString(LastName, Encoding.UTF8);
            byteBuf.WriteString(Email, Encoding.UTF8);
            byteBuf.WriteInt(Gender);
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
