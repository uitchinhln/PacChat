using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network
{
    public class SessionRegistry
    {
        ConcurrentDictionary<Guid, ChatSession> sessions = new ConcurrentDictionary<Guid, ChatSession>();

        public void Pulse()
        {
            foreach(ChatSession session in sessions.Values)
            {
                //session.pulse();
            }
        }

        public void Add(ChatSession session)
        {
            sessions.TryAdd(session.SessionID, session);
        }

        public void Remove(Guid id)
        {
            sessions.TryRemove(id, out var a);
        }

        public ChatSession Get(Guid id)
        {
            sessions.TryGetValue(id, out var result);
            return result;
        }
    }
}
