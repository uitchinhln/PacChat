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
    public class ConversationFrIDResponse : IPacket
    {
        public string ConversationID { get; set; }
        public string ConversationName { get; set; }
        public int StatusCode { get; set; }
        public long LastActive { get; set; }
        public HashSet<string> Members { get; set; } = new HashSet<string>();
        public int LastMessID { get; set; }
        public int PreviewCode { get; set; }
        public string PreviewContent { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, ConversationID); 
            ByteBufUtils.WriteUTF8(byteBuf, ConversationName); 
            byteBuf.WriteInt(StatusCode);
            byteBuf.WriteLong(LastActive);

            foreach (var member in Members)
                ByteBufUtils.WriteUTF8(byteBuf, member);
            ByteBufUtils.WriteUTF8(byteBuf, "~");

            byteBuf.WriteInt(LastMessID);
            byteBuf.WriteInt(PreviewCode);
            ByteBufUtils.WriteUTF8(byteBuf, PreviewContent);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
