using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.MessageCore.Message
{
    public class StickerMessage : AbstractMessage
    {
        [BsonElement("CategoryID")]
        public int CategoryID { get; set; }
        [BsonElement("StickerID")]
        public int StickerID { get; set; }

        public override int GetPreviewCode()
        {
            return 3;
        }
    }
}
