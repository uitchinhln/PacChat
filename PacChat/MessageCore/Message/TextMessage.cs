using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;

namespace PacChat.MessageCore.Message
{
    public class TextMessage : AbstractMessage
    {
        public string Message { get; set; }

        public override void DecodeFromBuffer(IByteBuffer buffer)
        {
            Message = ByteBufUtils.ReadUTF8(buffer);
        }

        public override IByteBuffer EncodeToBuffer(IByteBuffer buffer)
        {
            buffer.WriteInt(GetPreviewCode());
            ByteBufUtils.WriteUTF8(buffer, Message);
            return buffer;
        }

        public override int GetPreviewCode()
        {
            return 4;
        }
    }
}
