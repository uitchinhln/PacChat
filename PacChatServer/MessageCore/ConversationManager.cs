using PacChatServer.Cache.Core;
using PacChatServer.IO.Message;
using PacChatServer.MessageCore.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.MessageCore
{
    public static class ConversationManager
    {
        private static LRUCache<Guid, AbstractConversation> CachedConversation = new LRUCache<Guid, AbstractConversation>(1000, 10); 


        public static AbstractConversation GetConversation(Guid id)
        {
            CachedConversation.TryGet(id, out var result);

            if (result == null)
            {
                result = new ConversationStore().Load(id);
                CachedConversation.AddReplace(id, result);
            }

            return result;
        }
    }
}
