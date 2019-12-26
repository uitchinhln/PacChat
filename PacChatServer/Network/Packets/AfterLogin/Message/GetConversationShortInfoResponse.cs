using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Message
{
    public class GetConversationShortInfoResponse : IPacket
    {
        public string ConversationID { get; set; }
        public string ConversationName { get; set; }
        public long LastActive { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, ConversationID);
            ByteBufUtils.WriteUTF8(byteBuf, ConversationName);
            byteBuf.WriteLong(LastActive);
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
