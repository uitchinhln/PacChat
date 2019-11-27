using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChat.MessageCore.Sticker;

namespace PacChat.MessageCore.Message
{
    public class StickerMessage : AbstractMessage
    {
        public Sticker.Sticker Sticker { get; set; }

        public override void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public override IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
