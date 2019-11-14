using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.AfterLoginRequest
{
    public class GetIDs : IPacket
    {
        public void Decode(IByteBuffer buffer)
        {
            //throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            //throw new NotImplementedException();
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            
        }
    }
}
