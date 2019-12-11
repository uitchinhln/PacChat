using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MessageCore.Message;

namespace PacChat.Utils
{
    public class BubbleInfo
    {
        public AbstractMessage Message;
        public bool OnLeft;

        public BubbleInfo(AbstractMessage msg, bool lft)
        {
            Message = msg;
            OnLeft = lft;
        }
    }

    public class ConversationBubble
    {
        public List<string> Members { get; set; } = new List<string>();
        public int LastMessID { get; set; }
        public string ConversationName { get; set; }
        public List<BubbleInfo> Bubbles = new List<BubbleInfo>();
    }

    public enum BubbleType
    {
        Attachment = 1,
        Sticker = 3, 
        Text = 4
    }

    public class BubbleTypeParser
    {
        public static BubbleType Parse(AbstractMessage message)
        {
            return (BubbleType)message.GetPreviewCode();
        }
    }
}
