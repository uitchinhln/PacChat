using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChatServer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Notification
{
    public class GetNotificationsRequest : IPacket
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

            GetNotificationsResponse packet = new GetNotificationsResponse();
            ChatUser user = ChatUserManager.LoadUser(chatSession.Owner.ID);

            foreach (var noti in user.Notifications)
            {
                packet.Notifications.Add(noti);
            }

            chatSession.Send(packet);
        }
    }
}
