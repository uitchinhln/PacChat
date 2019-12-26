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
    public class SendMessage : IPacket
    {
        public string ConversationID { get; set; }
        public AbstractMessage Message { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            // throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, ConversationID);
            byteBuf = Message.EncodeToBuffer(byteBuf);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
        }
    }
}
