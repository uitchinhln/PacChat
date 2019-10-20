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
        ConcurrentDictionary<ChatSession, bool> sessions = new ConcurrentDictionary<ChatSession, bool>();

        public void Pulse()
        {
            foreach(ChatSession session in sessions.Keys)
            {
                //session.pulse();
            }
        }

        public void Add(ChatSession session)
        {
            sessions.TryAdd(session, true);
        }

        public void Remove(ChatSession session)
        {
            bool a;
            sessions.TryRemove(session, out a);
        }
    }
}
