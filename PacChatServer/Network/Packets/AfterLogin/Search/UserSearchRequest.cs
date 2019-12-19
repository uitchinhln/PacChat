using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity;
using PacChatServer.Entity.EntityProperty;
using PacChatServer.IO.Entity;
using PacChatServer.IO.Message;
using PacChatServer.MessageCore.Conversation;
using PacChatServer.MessageCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Search
{
    public class UserSearchRequest : IPacket
    {
        public String Email { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            Email = ByteBufUtils.ReadUTF8(buffer).ToLower();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            UserSearchResponse response = new UserSearchResponse();
            List<String> UserIDs = new ChatUserStore().SearchUserIDByEmail(Email, (session as ChatSession).Owner);

            ConversationStore store = new ConversationStore();

            foreach (var item in UserIDs)
            {
                Guid userId = Guid.Parse(item);
                ChatUser targetUser = ChatUserManager.LoadUser(userId);
                SearchResult result = new SearchResult();

                result.ID = targetUser.ID.ToString();
                result.FirstName = targetUser.FirstName;
                result.LastName = targetUser.LastName;
                result.IsOnline = targetUser.IsOnline();
                result.LastLogout = targetUser.LastLogoff;


                result.PreviewCode = -1;
                result.ConversationID = "~";

                var CommonConversation = targetUser.ConversationID.Intersect(chatSession.Owner.ConversationID);

                foreach (Guid id in CommonConversation)
                {
                    AbstractConversation conversation = store.Load(id);
                    if (conversation is SingleConversation)
                    {
                        result.ConversationID = conversation.ID.ToString();

                        AbstractMessage message =
                            conversation.MessagesID.Count > 0 ?
                            new MessageStore().Load(conversation.MessagesID.Last(), conversation.ID) :
                            null;
                        if (message == null) break;

                        if (!message.Showable(chatSession.Owner.ID))
                        {
                            result.PreviewCode = 0;
                            break;
                        }

                        result.PreviewCode = message.GetPreviewCode();

                        if (message.GetPreviewCode() == 4)
                        {
                            result.LastMessage = (message as TextMessage).Message;
                        }

                        break;
                    }
                }

                if (chatSession.Owner.Relationship.ContainsKey(targetUser.ID))
                {
                    result.Relationship = (int)Relation.Get(chatSession.Owner.Relationship[targetUser.ID]).RelationType;
                }
                else
                {
                    result.Relationship = (int)Relation.Type.None;
                }

                response.Results.Add(result);
            }

            session.Send(response);
        }
    }
}
