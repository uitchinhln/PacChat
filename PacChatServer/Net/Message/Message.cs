using System;

namespace PacChatServer.Net.Message
{
    interface Message
    {
        String toString();

        bool equal(Object obj);

        int hashCode();
    }
}
