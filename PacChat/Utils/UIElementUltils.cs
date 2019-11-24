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
        public IMessage message;
        public bool onLeft;

        public BubbleInfo(IMessage msg, bool lft)
        {
            message = msg;
            onLeft = lft;
        }
    }

}
