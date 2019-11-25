using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MessageCore.Sticker;

namespace PacChat.MessageCore.Message
{
    public class StickerMessage : AbstractMessage
    {
        public Sticker.Sticker Sticker { get; set; }
    }
}
