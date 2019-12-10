using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.IO.Message;
using PacChatServer.MessageCore.Conversation;
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

            ConversationFrIDResponse packet = new ConversationFrIDResponse();
            packet.ConversationID = ConversationID.ToString();
            var conversationStore = new ConversationStore().Load(ConversationID);

            packet.LastActive = 0;
            packet.ConversationName = "";

            if (conversationStore == null)
            {
                packet.StatusCode = 404;
            }
            else
            {
                packet.StatusCode = 200;
                packet.LastActive = conversationStore.LastActive;

                if (conversationStore is SingleConversation)
                    packet.ConversationName = "~";
                else
                    packet.ConversationName = conversationStore.ConversationName;

                foreach (var member in conversationStore.Members)
                {
                    packet.Members.Add(member.ToString());
                }

                packet.LastMessID = conversationStore.MessagesID.Count - 1;
            }

            // Update later
            packet.PreviewCode = -1;
            packet.PreviewContent = "";

            session.Send(packet);
        }
    }
}
