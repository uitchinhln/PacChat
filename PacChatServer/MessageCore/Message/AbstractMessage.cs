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
    public abstract class AbstractMessage : IMessage
    {
        /// <summary>
        /// 0: Text Message
        /// 1: Image Message
        /// 2: Video Message
        /// 3: Sticker Message
        /// 4: Attachment Message
        /// </summary>
        public int MessageType { get; protected set; } = 0;

        //ID of sender, sender is user
        public Guid SenderID { get; set; }

        //List of user seen this message
        public List<Guid> SeenBy { get; private set; } = new List<Guid>();

        public bool Revoked { get; set; } = false;

        //IDs of users who hide this message away from their conversation, the message still show to the others
        public List<Guid> HideBy { get; private set; } = new List<Guid>();

        //IDs of user who reacted and react id;
        public Dictionary<Guid, int> Reacts { get; private set; } = new Dictionary<Guid, int>();

        public void Hide(ChatUser user)
        {
            if (HideBy.Contains(user.ID)) return;
            HideBy.Add(user.ID);
        }

        public bool React(ChatUser user, int reactID)
        {
            if (Reacts.ContainsKey(user.ID)) return false;
            Reacts.Add(user.ID, reactID);
            return true;
        }

        public void Seen(ChatUser user)
        {
            if (SeenBy.Contains(user.ID)) return;
            SeenBy.Add(user.ID);
        }
    }
}
