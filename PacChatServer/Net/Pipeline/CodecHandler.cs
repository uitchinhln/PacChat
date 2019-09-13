using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;

namespace PacChatServer.Net.Handler
{
    class CodecHandler : MessageToMessageCodec<IByteBuffer, Message.Message>
    {
        protected override void Decode(IChannelHandlerContext ctx, IByteBuffer msg, List<object> output)
        {
            throw new NotImplementedException();
        }

        protected override void Encode(IChannelHandlerContext ctx, Message.Message msg, List<object> output)
        {
            throw new NotImplementedException();
        }
    }
}
