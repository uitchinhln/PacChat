using CNetwork.Protocols;
using CNetwork.Sessions;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network
{
    public class Session : BasicSession
    {
        public Session(IChannel channel, AbstractProtocol bootstrapProtocol) : base(channel, bootstrapProtocol)
        {

        }
    }
}
