using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNetwork.Utils;
using DotNetty.Buffers;

namespace PacChat.MessageCore.Message
{
    public class AttachmentMessage : AbstractMessage
    {
        public string FileID { get; set; }
        public string FileName { get; set; }

        public override void DecodeFromBuffer(IByteBuffer buffer)
        {
            FileID = ByteBufUtils.ReadUTF8(buffer);
            FileName = ByteBufUtils.ReadUTF8(buffer);
        }

        public override IByteBuffer EncodeToBuffer(IByteBuffer buffer)
        {
            buffer.WriteInt(GetPreviewCode());
            ByteBufUtils.WriteUTF8(buffer, FileID);
            ByteBufUtils.WriteUTF8(buffer, FileName);
            return buffer;
        }

        public override int GetPreviewCode()
        {
            return 1;
        }
    }
}
