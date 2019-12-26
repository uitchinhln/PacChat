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
    public class GetBoughtStickerPacksRequest : IPacket
    {
        public void Decode(IByteBuffer buffer)
        {
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            return byteBuf;
        }

        public void Handle(ISession session)
        {
        }
    }
}
