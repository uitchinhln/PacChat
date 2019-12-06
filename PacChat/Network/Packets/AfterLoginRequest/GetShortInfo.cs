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
    public class GetShortInfo : IPacket
    {
        public string ID { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            //throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            CNetwork.Utils.ByteBufUtils.WriteUTF8(byteBuf, ID);
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            
        }
    }
}
