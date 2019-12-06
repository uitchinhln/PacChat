using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;

namespace PacChat.MessageCore.Message
{
    public class TextMessage : AbstractMessage
    {
        public string Message { get; set; }

        public override void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public override IByteBuffer Encode(IByteBuffer byteBuf)
        {
            ByteBufUtils.WriteUTF8(byteBuf, ReceiverID);
            ByteBufUtils.WriteUTF8(byteBuf, Message);

            return byteBuf;
        }

        public override void Handle(ISession session)
        {
            throw new NotImplementedException();
        }

        public override void Reply()
        {
            throw new NotImplementedException();
        }

        public override void SendTo(string receiverID)
        {
            ReceiverID = receiverID;


        }
    }
}
