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
        protected HashSet<Guid> Members = new HashSet<Guid>();

        protected List<Guid> MessagesID = new List<Guid>();

        protected Dictionary<Guid, IMessage> Messages = new Dictionary<Guid, IMessage>();
    }
}
