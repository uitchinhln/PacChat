using CNetwork.Utils;
using DotNetty.Buffers;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.MessageCore.Message
{
    public class ImageMessage : AbstractMessage
    {
        [BsonElement("FileID")]
        public String FileID { get; set; }
        [BsonElement("FileName")]
        public String FileName { get; set; }

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
            return 2;
        }
    }
}
