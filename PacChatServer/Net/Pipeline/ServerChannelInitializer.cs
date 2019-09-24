using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using PacChatServer.Net.Handler;

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
            MessageHandler handler = new MessageHandler();
            CodecHandler codec = new CodecHandler();

            channel.Pipeline
                .AddLast("timeout", new IdleStateHandler(READ_IDLE_TIMEOUT, WRITE_IDLE_TIMEOUT, 0))
                .AddLast("codec", codec)
                .AddLast("hanlder", handler);
        }
    }
}
