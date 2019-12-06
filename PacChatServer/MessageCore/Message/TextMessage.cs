using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.MessageCore.Message
{
    public class TextMessage : AbstractMessage
    {
        [BsonElement("Text")]
        public String Message { get; set; }

        public override int GetPreviewCode()
        {
            return 4;
        }
    }
}
