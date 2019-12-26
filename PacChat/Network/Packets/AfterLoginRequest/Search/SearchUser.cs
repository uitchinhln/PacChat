using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.AfterLoginRequest.Search
{
    public class SearchUser : IPacket
    {
        public string Email { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, Email);
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
