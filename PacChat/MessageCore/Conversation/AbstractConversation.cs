using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MessageCore;

namespace PacChat.MessageCore.Conversation
{
    public abstract class AbstractConversation : IConversation
    {
        public Guid ID { get; set; }
        public long LastActive { get; set; }
        public HashSet<Guid> Members { get; set; } = new HashSet<Guid>();
        public List<Guid> MessagesID { get; set; } = new List<Guid>();
        public Dictionary<Guid, IMessage> LoadedMessages { get; set; } = new Dictionary<Guid, IMessage>();
    }
}
