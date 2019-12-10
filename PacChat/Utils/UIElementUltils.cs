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
        public List<string> Members { get; set; } = new List<string>();
        public int LastMessID { get; set; }
        public string ConversationName { get; set; }
        public List<BubbleInfo> Bubbles = new List<BubbleInfo>();
    }
}
