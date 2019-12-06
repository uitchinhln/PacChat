using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.MessageCore
{
    public interface IMessage
    {
        void SendTo(string receiverID);
        void Reply();
    }
}
