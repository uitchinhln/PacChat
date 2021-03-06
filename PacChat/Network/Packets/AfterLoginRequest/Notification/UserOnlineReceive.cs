﻿using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChat.ChatPageContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.Network.Packets.AfterLoginRequest.Notification
{
    public class UserOnlineReceive : IPacket
    {
        public string UserID { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            UserID = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            PacChat.Utils.Packets.SendPacket<GetFriendIDs>();
            return;

            var app = MainWindow.chatApplication;
            var friend = app.model.UserControls[UserID] as UserMessage;

            if (app.model.IsOnline.ContainsKey(UserID))
                app.model.IsOnline[UserID] = false;

            Application.Current.Dispatcher.Invoke(() => friend.SetOnlineStatus(true));
        }
    }
}
