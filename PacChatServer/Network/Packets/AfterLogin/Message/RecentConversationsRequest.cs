using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Message
{
    public class RecentConversationsRequest : IPacket
    {
        public void Decode(IByteBuffer buffer)
        {
            // throw new NotImplementedException();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            RecentConversationsResponse packet = new RecentConversationsResponse();

            foreach (var conversation in chatSession.Owner.Conversations)
            {
                packet.Conversations.Add(conversation.Key, conversation.Value);
            }

            session.Send(packet);
        }
    }
}
