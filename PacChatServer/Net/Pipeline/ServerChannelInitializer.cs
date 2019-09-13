using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace PacChatServer.Net.Pipeline
{
    class ServerChannelInitializer : ChannelInitializer<ISocketChannel>
    {
        private static int READ_IDLE_TIMEOUT = 20;
        private static int WRITE_IDLE_TIMEOUT = 15;

        ChatServer connectionManager;

        public ServerChannelInitializer(ChatServer connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        protected override void InitChannel(ISocketChannel channel)
        {
            
            //channel.Pipeline.AddLast();
        }
    }
}
