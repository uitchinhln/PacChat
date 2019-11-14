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
    public class GetIDsResult : IPacket
    {
        public List<int> ids { get; set; } = new List<int>();
        public void Decode(IByteBuffer buffer)
        {
            int id = buffer.ReadInt();
            while (id != -1402)
            {
                ids.Add(id);
                id = buffer.ReadInt();
            }    
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            foreach (int id in ids)
            {
                Console.WriteLine(id);
            }    
        }
    }
}
