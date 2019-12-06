using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MessageCore.Message;

namespace PacChat.Utils
{
    public struct BubbleInfo
    {
        public string message;
        public bool onLeft;

        public BubbleInfo(string msg, bool lft)
        {
            message = msg;
            onLeft = lft;
        }
    }

    public class ConversationBubble
    {
        public string ConversationID { get; set; }
        public List<BubbleInfo> Bubbles = new List<BubbleInfo>();

        public ConversationBubble(string convID)
        {
            ConversationID = convID;
        }
    }

}
