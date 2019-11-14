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
    public class GetInfo : IPacket
    {
        public int Id { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            //throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            byteBuf.WriteInt(Id);

            return byteBuf;
        }

        public void Handle(ISession session)
        {
            
        }
    }
}
