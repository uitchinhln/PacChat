using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChatServer.Entity;
using PacChatServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Notification
{
    public class AddFriendRequest : IPacket
    {
        public string TargetID { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            TargetID = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatSession chatSession = session as ChatSession;

            ChatUser user;
            if (ChatUserManager.OnlineUsers.TryGetValue(Guid.Parse(TargetID), out user))
            {
                ForwardedFriendRequest packet = new ForwardedFriendRequest();
                packet.SenderID = chatSession.Owner.ID.ToString();
                packet.Name = chatSession.Owner.FirstName + " " + chatSession.Owner.LastName;
                Console.WriteLine("Friend request " + packet.Name);
                user.Send(packet);
            }

            string name = chatSession.Owner.FirstName + " " + chatSession.Owner.LastName;

            //string encNoti = "mkfriend:" + chatSession.Owner.ID + ":" +
            //    chatSession.Owner.FirstName + " " + chatSession.Owner.LastName;

            string encNoti = NotificationEncoder.Assemble(
                NotificationPrefixes.AddFriend,
                chatSession.Owner.ID.ToString(),
                name, name, "sent you a friend request.",
                false);

            user = ChatUserManager.LoadUser(Guid.Parse(TargetID));
            user.Notifications.Add(encNoti);
            user.Save();
        }
    }
}
