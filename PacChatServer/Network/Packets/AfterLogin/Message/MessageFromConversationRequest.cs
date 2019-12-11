using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.IO.Message;
using PacChatServer.MessageCore.Conversation;
using PacChatServer.MessageCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Message
{
    public class MessageFromConversationRequest : IPacket
    {
        public Guid ConversationID { get; set; }
        public int MessagePosition { get; set; }
        public int Quantity { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            ConversationID = Guid.Parse(ByteBufUtils.ReadUTF8(buffer));
            MessagePosition = buffer.ReadInt();
            Quantity = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            if (MessagePosition == -1) return;

            ChatSession chatSession = session as ChatSession;
            ConversationStore store = new ConversationStore();

            AbstractConversation conversation = store.Load(ConversationID);

            MessageFromConversationResponse packet = new MessageFromConversationResponse();

            var messages = conversation.MessagesID;

            if (messages.Count == 0) return;
            MessageStore messageStore = new MessageStore();

            for (int i = MessagePosition; i >= Math.Max(0, MessagePosition - Quantity + 1); --i)
            {
                AbstractMessage mess = new MessageStore().Load(messages[i], ConversationID);

                if (mess != null)
                {
                    packet.SenderID.Add(mess.Author.ToString());
                    packet.Content.Add(mess);
                }
            }

            chatSession.Owner.Send(packet);
        }
    }
}
