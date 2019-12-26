using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Sticker
{
    public class GetNearestSickerResponse : IPacket
    {
        public List<int> NearestSticker { get; set; } = new List<int>();

        public void Decode(IByteBuffer buffer)
        {
            throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            foreach (var stickerID in NearestSticker)
            {
                byteBuf.WriteInt(stickerID);
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
