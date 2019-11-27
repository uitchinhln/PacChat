using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity;
using PacChatServer.IO.Message;
using PacChatServer.MessageCore.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Message
{
    public class ConversationFromFriendIDRequest : IPacket
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

            ConversationStore store = new ConversationStore();
            SingleConversation resultConv = null;

            foreach (Guid id in chatSession.Owner.ConversationID)
            {
                if (targetUser.ConversationID.Contains(id))
                {
                    AbstractConversation conversation = store.Load(id);
                    if (conversation is SingleConversation)
                    {
                        resultConv = conversation as SingleConversation;
                        break;
                    }
                }
            }

            ConversationFromFriendIDResponse response = new ConversationFromFriendIDResponse();

            if (resultConv != null)
            {
                response.ConversationID = resultConv.ID.ToString();
            }

            chatSession.Send(response);
        }
    }
}
