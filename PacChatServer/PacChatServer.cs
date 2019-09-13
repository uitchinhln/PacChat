using DotNetty.Transport.Channels;
using PacChatServer.Net;
using PacChatServer.Net.Protocol;
using System.Net;
using System.Threading.Tasks;

namespace PacChatServer
{
    class PacChatServer
    {
        public PacChatServer()
        {
            ChatServer server = new ChatServer(this, new ProtocolProvider());
            Task<IChannel> channel = server.BindAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1515));

        }
    }
}
