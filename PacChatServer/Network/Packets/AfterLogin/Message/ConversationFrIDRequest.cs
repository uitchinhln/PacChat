using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Message
{
    public class ConversationFrIDRequest : IPacket
    {
        public Guid ConversationID { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            ConversationID = Guid.Parse(ByteBufUtils.ReadUTF8(buffer));
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;


        }
    }
}
