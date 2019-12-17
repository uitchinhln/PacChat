using MongoDB.Bson.Serialization.Attributes;
using PacChatServer.Cache.Core;
using PacChatServer.Entity;
using PacChatServer.IO.Entity;
using PacChatServer.IO.Message;
using PacChatServer.MessageCore.Message;
using PacChatServer.Network;
using PacChatServer.Network.Packets.AfterLogin.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.MessageCore.Conversation
{
    /// <summary>
    /// Pattern:
    ///     - Template method: Deploy between Conversation Classes
    ///     - Mediator: Deploy between user class and conversation class (ex: send, receive)
    ///     - Observer: Deploy between user class and conversation class (ex: join, leave, play music...)
    ///     - Bridge: Deploy between message class and conversation class
    /// </summary>
    [BsonKnownTypes(typeof(SingleConversation), typeof(GroupConversation))]
    public abstract class AbstractConversation : IConversation
    {
        ConversationStore store = new ConversationStore();
        MessageStore messageStore = new MessageStore();

        [BsonId]
        public Guid ID { get; set; }

        [BsonElement("ConversationName")]
        public string ConversationName { get; set; }

        [BsonElement("LastActive")]
        public long LastActive { get; set; }

        [BsonElement("Members")]
        public HashSet<Guid> Members { get; set; } = new HashSet<Guid>();

        [BsonElement("MessagesID")]
        public List<Guid> MessagesID { get; set; } = new List<Guid>();

        [BsonElement("MediaID")]
        public List<Guid> MediaID { get; set; } = new List<Guid>();

        [BsonElement("AttachmentID")]
        public List<Guid> AttachmentID { get; set; } = new List<Guid>();

        [BsonIgnore]
        public LRUCache<Guid, IMessage> LoadedMessages { get; set; } = new LRUCache<Guid, IMessage>(100, 10);

        public void SendMessage(AbstractMessage message, ChatSession chatSession)
        {
            ChatUser user;
            Guid messageID = Guid.NewGuid();
            (message as AbstractMessage).ID = messageID;
            (message as AbstractMessage).Author = chatSession.Owner.ID;

            //Store
            if (message is VideoMessage || message is ImageMessage)
                MediaID.Add((message as AbstractMessage).ID);
            if (message is AttachmentMessage)
                AttachmentID.Add((message as AbstractMessage).ID);
            if (message is StickerMessage)
            {
                chatSession.Owner.NearestStickers.Add((message as StickerMessage).StickerID);
                chatSession.Owner.Save();
            }

            MessagesID.Add((message as AbstractMessage).ID);
            LoadedMessages.AddReplace((message as AbstractMessage).ID, message);

            store.UpdateMessagesList(this);
            messageStore.Save((message as AbstractMessage), this.ID);

            foreach (Guid userID in Members)
            {
                this.LastActive = DateTime.Now.Ticks;

                if (ChatUserManager.OnlineUsers.TryGetValue(userID, out user))
                {
                    if (userID.CompareTo(chatSession.Owner.ID) == 0)
                        continue;

                    SendMessageResponse packet = new SendMessageResponse();
                    packet.ConversationID = this.ID.ToString();
                    packet.Message = message;
                    packet.SenderID = chatSession.Owner.ID.ToString();
                    user.Send(packet); //Add message packet here

                    lock (user.Conversations)
                    {
                        long err;
                        user.Conversations.TryRemove(ID, out err);
                        user.Conversations.TryAdd(ID, LastActive);
                    }
                }
            }
        }

        public void UpdateLastActive(ChatSession chatSession)
        {
            LastActive = long.MaxValue;

            foreach (var member in Members)
            {
                if (member.Equals(chatSession.Owner.ID)) continue;

                if (ChatUserManager.OnlineUsers.ContainsKey(member))
                {
                    LastActive = 0;
                    break;
                }

                ChatUser user = new ChatUserStore().Load(member);
                LastActive = Math.Min(LastActive, (long)Math.Ceiling((DateTime.UtcNow - user.LastLogoff).TotalMinutes));
            }
        }
    }
}
