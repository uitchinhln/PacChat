using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PacChat.Utils;

namespace PacChat.Network.Packets.AfterLoginRequest.Notification
{
    public class FriendRequestResult : IPacket
    {
        public string UserID { get; set; }
        public string Name { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            UserID = ByteBufUtils.ReadUTF8(buffer);
            Name = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            // Notify
            Application.Current.Dispatcher.Invoke(() =>
                NotificationPage.Instance.AddFriendAcceptedNoti(UserID, Name));

            PacChat.Utils.Packets.SendPacket<GetFriendIDs>();   
        }
    }
}
