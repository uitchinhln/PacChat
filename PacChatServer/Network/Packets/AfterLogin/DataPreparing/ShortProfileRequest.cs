using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity;
using PacChatServer.Entity.EntityProperty;
using PacChatServer.IO.Message;
using PacChatServer.MessageCore.Conversation;
using PacChatServer.MessageCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.DataPreparing
{
    public class ShortProfileRequest : IPacket
    {
        public Guid TargetID { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            TargetID = Guid.Parse(ByteBufUtils.ReadUTF8(buffer));
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            ChatUser targetUser = ChatUserManager.LoadUser(TargetID);

            if (targetUser == null) return;
            
            ShortProfileResponse response = new ShortProfileResponse();

            response.ID = targetUser.ID.ToString();
            response.FirstName = targetUser.FirstName;
            response.LastName = targetUser.LastName;
            response.IsOnline = targetUser.IsOnline();
            response.LastLogout = targetUser.LastLogoff;

            ConversationStore store = new ConversationStore();

            response.PreviewCode = -1;
            response.ConversationID = "~";

            var CommonConversation = targetUser.ConversationID.Intersect(chatSession.Owner.ConversationID);

            foreach (Guid id in CommonConversation)
            {
                AbstractConversation conversation = store.Load(id);
                if (conversation is SingleConversation)
                {
                    response.ConversationID = conversation.ID.ToString();

                    AbstractMessage message = new MessageStore().Load(conversation.MessagesID.Last(), conversation.ID);
                    if (message == null) break;

                    if (message.Showable(chatSession.Owner.ID))
                    {
                        response.PreviewCode = 0;
                        break;
                    }

                    response.PreviewCode = message.GetPreviewCode();

                    if (message.GetPreviewCode() == 4)
                    {
                        response.LastMess = (message as TextMessage).Message;
                    }

                    break;
                }
            }

            if (chatSession.Owner.Relationship.ContainsKey(targetUser.ID))
            {
                response.RelationshipType = (int) Relation.Get(chatSession.Owner.Relationship[targetUser.ID]).RelationType;
            } else
            {
                response.RelationshipType = (int) Relation.Type.None;
            }

            chatSession.Send(response);
        }
    }
}
