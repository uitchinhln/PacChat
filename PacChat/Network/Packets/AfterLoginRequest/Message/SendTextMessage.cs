using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChat.MessageCore.Message;
using PacChat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class SendTextMessage : IPacket
    {
        public string ConversationID { get; set; }
        public string TargetID { get; set; }
        public TextMessage Message { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            // throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, ConversationID);
            ByteBufUtils.WriteUTF8(byteBuf, TargetID);
            ByteBufUtils.WriteUTF8(byteBuf, Message.Message);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
        }
    }
}
