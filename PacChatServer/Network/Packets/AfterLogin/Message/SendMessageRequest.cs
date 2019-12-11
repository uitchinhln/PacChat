using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity;
using PacChatServer.IO.Message;
using PacChatServer.MessageCore;
using PacChatServer.MessageCore.Conversation;
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
        public Guid ConversationID { get; set; }
        public int PreviewCode { get; set; }
        public AbstractMessage Message { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            ConversationID = Guid.Parse(ByteBufUtils.ReadUTF8(buffer));
            PreviewCode = buffer.ReadInt();

            if (PreviewCode == 4)
            {
                Message = new TextMessage();
                (Message as TextMessage).Message = ByteBufUtils.ReadUTF8(buffer);
            }

            if (PreviewCode == 3)
            {
                Message = new StickerMessage();
                (Message as StickerMessage).StickerID = buffer.ReadInt();
                (Message as StickerMessage).CategoryID = buffer.ReadInt();
            }
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            AbstractConversation conversation = ConversationManager.GetConversation(ConversationID);

            if (conversation == null) return;

            conversation.SendMessage(Message, chatSession);

            /*
            if (ChatUserManager.OnlineUsers.TryGetValue(Guid.Parse(ReceiverID), out user))
            {
                SendMessageResponse packet = new SendMessageResponse();
                packet.ConversationID = ConversationID;
                packet.Message = Message;
                packet.SenderID = chatSession.Owner.ID.ToString();
                user.Send(packet);
            }
            */
        }
    }
}
