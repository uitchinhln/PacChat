using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MessageCore.Message;

namespace PacChat.Utils
{
    //{
    //    {0, "Hidden message" },
    //    {1, "Attachment" },
    //    {2, "You got an image message" },
    //    {3, "You got a sticker message" },
    //    {5, "Video"}
    public enum MessageType
    {
        Hidden = 0,
        Attachment = 1,
        Image = 2,
        StickerMsg = 3,
        Text = 4,
        Video = 5
    }

    public struct BubbleInfo
    {
        public AbstractMessage message;
        public bool onLeft;

        public BubbleInfo(AbstractMessage msg, bool lft)
        {
            message = msg;
            onLeft = lft;
        }
    }

    public class ConversationBubble
    {
        public List<string> Members { get; set; } = new List<string>();
        public int LastMessID { get; set; }
        public List<BubbleInfo> Bubbles = new List<BubbleInfo>();
    }
}
