using CNetwork.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Protocol
{
    public class ProtocolProvider
    {
        public PreLoginProtocol Test { get; }
        public ProtocolProvider()
        {
            this.Test = new PreLoginProtocol();
        }
    }
}
