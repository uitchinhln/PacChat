using MongoDB.Bson.Serialization.Attributes;
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
        [BsonId]
        public Guid ID { get; set; }

        [BsonElement("Members")]
        public HashSet<Guid> Members { get; set; } = new HashSet<Guid>();

        [BsonElement("MessagesID")]
        public List<Guid> MessagesID { get; set; } = new List<Guid>();

        [BsonIgnore]
        public Dictionary<Guid, IMessage> LoadedMessages { get; set; } = new Dictionary<Guid, IMessage>();
    }
}
