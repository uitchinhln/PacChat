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
        //ID of sender, sender is user
        public Guid SenderID { get; set; }

        //List of user seen this message
        public HashSet<Guid> SeenBy { get; private set; } = new HashSet<Guid>();

        public bool Revoked { get; set; } = false;

        //IDs of users who hide this message away from their conversation, the message still show to the others
        public HashSet<Guid> HideBy { get; private set; } = new HashSet<Guid>();

        //IDs of user who reacted and react id;
        public Dictionary<Guid, int> Reacts { get; private set; } = new Dictionary<Guid, int>();

        public void Hide(ChatUser user)
        {
            if (HideBy.Contains(user.ID)) return;
            HideBy.Add(user.ID);
        }

        public void React(ChatUser user, int reactID)
        {
            Reacts.Add(user.ID, reactID);
        }

        public void Seen(ChatUser user)
        {
            SeenBy.Add(user.ID);
        }
    }
}
