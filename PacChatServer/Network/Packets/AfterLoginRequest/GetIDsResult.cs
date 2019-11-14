using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLoginRequest
{
    public class GetIDsResult : IPacket
    {
        public List<int> ids { get; set; } = new List<int>();
        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            foreach (int id in ids)
            {
                byteBuf.WriteInt(id);
            }
            byteBuf.WriteInt(-1402);
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
