using DotNetty.Transport.Channels;
using PacChatServer.Net.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Net.Pipeline
{
    class MessageHandler : SimpleChannelInboundHandler<Message>
    {


        protected override void ChannelRead0(IChannelHandlerContext ctx, Message msg)
        {
            throw new NotImplementedException();
        }

        public override void ChannelActive(IChannelHandlerContext ctx)
        {
            IChannel c = ctx.Channel;
                
        }


        public override void ChannelInactive(IChannelHandlerContext ctx)
        {
                
        }


        public override void UserEventTriggered(IChannelHandlerContext ctx, Object evt)
        {

        }

        public override void ExceptionCaught(IChannelHandlerContext ctx, Exception exception)
        {
            
        }
    }
}
