using DotNetty.Buffers;

namespace PacChatServer.Net.Interface
{
    interface Codec<T> where T : Message
    {
        T Decode(IByteBuffer byteBuf);

        IByteBuffer Encode(IByteBuffer byteBuf, T message);

        Codec<T> Clone();
    }
}
