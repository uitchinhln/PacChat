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
    public class MessageFromConversationResponse : IPacket
    {
        //Type of message
        /// <summary>
        /// 1: Attachment
        /// 2: Image
        /// 3: Sticker
        /// 4: Text
        /// 5: Video
        /// </summary>

        public List<string> SenderID { get; set; } = new List<string>();
        public List<int> MessType { get; set; } = new List<int>();

        // Text content is content of text message (if preview code is equal to 4)
        public List<string> Content { get; set; } = new List<string>();

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            for (int i = 0; i < SenderID.Count; ++i)
            {
                ByteBufUtils.WriteUTF8(byteBuf, SenderID[i]);
                byteBuf.WriteInt(MessType[i]);
                ByteBufUtils.WriteUTF8(byteBuf, Content[i]);
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
