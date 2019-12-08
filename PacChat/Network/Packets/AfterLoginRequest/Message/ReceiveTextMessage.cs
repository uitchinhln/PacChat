using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using PacChat.MessageCore.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class ReceiveTextMessage : IPacket
    {
        public string ConversationID { get; set; }
        public string SenderID { get; set; }
        public TextMessage Message { get; set; } = new TextMessage();

        public void Decode(IByteBuffer buffer)
        {
            ConversationID = ByteBufUtils.ReadUTF8(buffer);
            SenderID = ByteBufUtils.ReadUTF8(buffer);
            Message.Message = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            Console.WriteLine("Received" + ConversationID);

            Application.Current.Dispatcher.Invoke(() =>
            {
                var app = MainWindow.chatApplication;
                if (!app.model.Conversations.ContainsKey(ConversationID))
                {
                    app.model.Conversations.Add(ConversationID, new Utils.ConversationBubble());
                    var temp = app.model.Conversations[ConversationID];
                    temp.Members.Add(SenderID);
                }

                app.model.Conversations[ConversationID].Bubbles.Add(new Utils.BubbleInfo(Message.Message, true));
                app.model.PrivateConversations[SenderID] = ConversationID;

                if (app.model.currentSelectedConversation.CompareTo(ConversationID) == 0)
                    ChatPage.Instance.SendLeftMessages(Message, true);
            });
        }
    }
}
