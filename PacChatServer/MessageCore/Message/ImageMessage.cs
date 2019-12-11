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
            throw new NotImplementedException();
        }

        public override IByteBuffer EncodeToBuffer(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public override int GetPreviewCode()
        {
            return 2;
        }
    }
}
