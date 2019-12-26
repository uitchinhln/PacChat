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
    public class GetBoughtStickerPacksRequest : IPacket
    {
        public void Decode(IByteBuffer buffer)
        {

        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;
            GetBoughtStickerPacksResponse response = new GetBoughtStickerPacksResponse();
            response.BoughtStickerPacks.AddRange(chatSession.Owner.BoughtStickerPacks);
            chatSession.Send(response);
        }
    }
}
