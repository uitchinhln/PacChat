using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using PacChatServer.Net.Interface;

namespace PacChatServer.Net.Handler
{
    class CodecHandler : MessageToMessageCodec<IByteBuffer, Message>
    {
        protected override void Decode(IChannelHandlerContext ctx, IByteBuffer msg, List<object> output)
        {
            throw new NotImplementedException();
        }

        protected override void Encode(IChannelHandlerContext ctx, Message msg, List<object> output)
        {
            //ctx.Allocator.Buffer();
            throw new NotImplementedException();
        }
    }
}
