using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.MessageCore.Message
{
    public interface IMessage
    {
        void SendTo(Guid receiver);
        void Reply();
    }
}
