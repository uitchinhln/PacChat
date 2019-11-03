using CNetwork;
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
    public class ClientSession : BasicSession
    {
        public ClientSession(IChannel channel, AbstractProtocol bootstrapProtocol) : base(channel, bootstrapProtocol)
        {

        }

        public override void Send(IPacket packet)
        {
            base.Send(packet);
            Console.WriteLine("Sending...");
        }
    }
}
