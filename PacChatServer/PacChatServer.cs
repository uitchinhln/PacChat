using DotNetty.Transport.Channels;
using PacChatServer.Net;
using PacChatServer.Net.Protocol;
using PacChatServer.Utils.ThreadUtils;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PacChatServer
{
    class PacChatServer
    {
        public PacChatServer()
        {
            CountdownLatch latch = new CountdownLatch(1);

            ChatServer server = new ChatServer(this, new ProtocolProvider(), latch);
            Task<IChannel> channel = server.BindAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1515));
            
            latch.Wait();

            if (channel != null)
            {
                _ = server.ShutdownAsync();
            }
        }
    }
}
