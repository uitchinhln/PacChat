using DotNetty.Buffers;

namespace PacChatServer.Net.Codec
{
    interface Codec<T> where T : Message.Message
    {
        T Decode(IByteBuffer byteBuf);

        IByteBuffer Encode(IByteBuffer byteBuf, T message);
    }
}
