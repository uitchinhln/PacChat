using MongoDB.Bson.Serialization.Attributes;
using PacChatServer.Cache.Core;
using PacChatServer.Entity;
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

        [BsonElement("LastActive")]
        public long LastActive { get; set; }

        [BsonElement("Members")]
        public HashSet<Guid> Members { get; set; } = new HashSet<Guid>();

        [BsonElement("MessagesID")]
        public List<Guid> MessagesID { get; set; } = new List<Guid>();

        [BsonIgnore]
        public LRUCache<Guid, IMessage> LoadedMessages { get; set; } = new LRUCache<Guid, IMessage>(100, 10);

        public void SendMessage(IMessage message, string conversationID, ChatSession chatSession)
        {
            ChatUser user;
            foreach (Guid userID in Members)
            {
                this.LastActive = DateTime.Now.Ticks;

                //Store
                MessagesID.Add((message as AbstractMessage).ID);
                LoadedMessages.AddReplace((message as AbstractMessage).ID, message);

                store.UpdateMessagesList(this);
                messageStore.Save((message as AbstractMessage), this.ID);


                if (ChatUserManager.OnlineUsers.TryGetValue(userID, out user))
                {
                    if (userID.CompareTo(chatSession.Owner.ID) == 0)
                        continue;

                    SendMessageResponse packet = new SendMessageResponse();
                    packet.ConversationID = conversationID;
                    packet.Message = message as TextMessage;
                    packet.SenderID = chatSession.Owner.ID.ToString();
                    user.Send(packet); //Add message packet here

                    lock (user.Conversations)
                    {
                        user.Conversations.Remove(ID);
                        user.Conversations.Add(ID, LastActive);
                    }
                }
            }
        }
    }
}
