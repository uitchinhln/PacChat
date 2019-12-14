using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChat.MessageCore.Sticker;

namespace PacChat.MessageCore.Message
{
    public class StickerMessage : AbstractMessage
    {
        public Sticker.Sticker Sticker { get; set; }

        public override void DecodeFromBuffer(IByteBuffer buffer)
        {
            Sticker = new Sticker.Sticker();
            Sticker.ID = buffer.ReadInt();
            //Sticker.CategoryID = buffer.ReadInt();
        }

        public override IByteBuffer EncodeToBuffer(IByteBuffer buffer)
        {
            buffer.WriteInt(GetPreviewCode());
            buffer.WriteInt(Sticker.ID);
            //buffer.WriteInt(Sticker.CategoryID);
            return buffer;
        }

        public override int GetPreviewCode()
        {
            return 3;
        }
    }
}
