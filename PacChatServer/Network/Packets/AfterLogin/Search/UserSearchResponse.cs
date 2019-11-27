using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Search
{
    public class UserSearchResponse : IPacket
    {
        public List<String> UserIDs = new List<String>();

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            foreach (string s in UserIDs)
            {
                ByteBufUtils.WriteUTF8(byteBuf, s);
            }
            ByteBufUtils.WriteUTF8(byteBuf, "~");

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
