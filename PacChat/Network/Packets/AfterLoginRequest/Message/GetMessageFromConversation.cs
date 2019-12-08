using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class GetMessageFromConversation : IPacket
    {
        public string ConversationID { get; set; }
        public int MessagePosition { get; set; }
        public int Quantity { get; set; }

        public void Decode(IByteBuffer buffer)
        {
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, ConversationID);
            byteBuf.WriteInt(MessagePosition);
            byteBuf.WriteInt(Quantity);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
        }
    }
}
