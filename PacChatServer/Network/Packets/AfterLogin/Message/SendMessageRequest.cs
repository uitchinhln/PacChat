using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity;
using PacChatServer.IO.Message;
using PacChatServer.MessageCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Message
{
    public class SendMessageRequest : IPacket
    {
        public string ConversationID { get; set; }
        public string ReceiverID { get; set; }
        public TextMessage Message { get; set; } = new TextMessage();

        public void Decode(IByteBuffer buffer)
        {
            ConversationID = ByteBufUtils.ReadUTF8(buffer);
            ReceiverID = ByteBufUtils.ReadUTF8(buffer);
            Message.Message = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;
            ChatUser user;

            if (ChatUserManager.OnlineUsers.TryGetValue(Guid.Parse(ReceiverID), out user))
            {
                SendMessageResponse packet = new SendMessageResponse();
                packet.ConversationID = ConversationID;
                packet.Message = Message;
                packet.SenderID = chatSession.Owner.ID.ToString();

                string mess = packet.Message.Message;
                for (int i = 0; i < 30; i++)
                {
                    packet.Message.Message = mess + " " + i;
                    user.Send(packet);
                }
            }

            new MessageStore().Save(Message, Guid.Parse(ConversationID));
        }
    }
}
