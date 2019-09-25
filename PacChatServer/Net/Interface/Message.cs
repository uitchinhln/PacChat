using System;

namespace PacChatServer.Net.Interface
{
    interface Message
    {
        String toString();

        bool equal(Object obj);

        int hashCode();

        Message Clone();
    }
}
