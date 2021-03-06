﻿using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class BuzzReceive : IPacket
    {
        public String SenderID { get; set; }
        public String ConversationID { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            SenderID = ByteBufUtils.ReadUTF8(buffer);
            ConversationID = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            var app = MainWindow.chatApplication;
            Application.Current.Dispatcher.Invoke(() => 
            {
                ChatPage.Instance.Buzz();
                if (MainWindow.Instance.WindowState == WindowState.Minimized)
                {
                    MainWindow.Instance.WindowState = MainWindow.Instance.isMaximized ? WindowState.Maximized : WindowState.Normal;
                }
                MainWindow.Instance.Activate();
                Console.WriteLine(ConversationID);
            });
        }
    }
}
