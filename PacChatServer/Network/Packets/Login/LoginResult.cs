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
    public class LoginResult : IPacket
    {
        public int StatusCode { get; set; }
        public string Token { get; set; }


        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            byteBuf.WriteInt(StatusCode);
            ByteBufUtils.WriteUTF8(byteBuf, Token);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
