using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.AfterLoginRequest.Sticker
{
    public class GetNearestSickerResponse : IPacket
    {
        public List<int> NearestSticker { get; set; } = new List<int>();

        public void Decode(IByteBuffer buffer)
        {
            int id = buffer.ReadInt();
            while (id > 0)
            {
                NearestSticker.Add(id);
                id = buffer.ReadInt();
            }
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            //Code vao day
        }
    }
}
