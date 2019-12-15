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
    public class GetNearestSickerRequest : IPacket
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
            GetNearestSickerResponse response = new GetNearestSickerResponse();
            response.NearestSticker.AddRange(chatSession.Owner.NearestStickers);
            chatSession.Send(response);
        }
    }
}
