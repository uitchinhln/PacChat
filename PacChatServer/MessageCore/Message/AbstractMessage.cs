using MongoDB.Bson.Serialization.Attributes;
using PacChatServer.Entity;
using PacChatServer.MessageCore.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.MessageCore.Message
{
    /// <summary>
    /// Pattern:
    ///     - Template Method
    ///     - Decorator: make the change role/type of message easily
    /// </summary>
    [BsonKnownTypes(typeof(AttachmentMessage), typeof(ImageMessage), typeof(StickerMessage), typeof(TextMessage), typeof(VideoMessage))]
    public abstract class AbstractMessage : IMessage
    {
        [BsonId]
        public Guid ID { get; set; }

        //ID of sender, sender is user
        [BsonElement("SenderID")]
        public Guid SenderID { get; set; }

        //List of user seen this message
        [BsonElement("SeenBy")]
        public HashSet<Guid> SeenBy { get; private set; } = new HashSet<Guid>();

        //if Revoked = true, the message will be hidden to all users.
        [BsonElement("Revoked")]
        public bool Revoked { get; set; } = false;

        //IDs of users who hide this message away from their conversation, the message still show to the others
        [BsonElement("HideBy")]
        public HashSet<Guid> HideBy { get; private set; } = new HashSet<Guid>();

        //IDs of user who reacted and react id;
        [BsonElement("Reacts")]
        public Dictionary<Guid, int> Reacts { get; private set; } = new Dictionary<Guid, int>();

        /// <summary>
        /// 1: Attachment
        /// 2: Image
        /// 3: Sticker
        /// 4: Text
        /// 5: Video
        /// </summary>
        public abstract int GetPreviewCode();

        public bool Showable(Guid id)
        {
            return !Revoked && !HideBy.Contains(id);
        }
    }
}
