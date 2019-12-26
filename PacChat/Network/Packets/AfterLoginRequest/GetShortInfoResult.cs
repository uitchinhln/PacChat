using CNetwork;
using CNetwork.Utils;
using CNetwork.Sessions;
using DotNetty.Buffers;
using PacChat.ChatAMVC;
using PacChat.ChatPageContents.ViewModels;
using PacChat.MVC;
using PacChat.Windows.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PacChat.ChatPageContents;
using PacChat.Utils;

namespace PacChat.Network.Packets.AfterLoginRequest
{
    public class GetShortInfoResult : IPacket
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LastMessage { get; set; }
        public string ConversationID { get; set; }
        public int PreviewCode { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastLogout { get; set; }
        public int Relationship { get; set; }

        private readonly string[] TranslatedPreviewCode = new string[6]
        {
            "Hidden message",
            "Attachment",
            "Image message",
            "Sticker",
            "-",
            "Video"
        };

        public void Decode(IByteBuffer buffer)
        {
            ID = ByteBufUtils.ReadUTF8(buffer);
            FirstName = ByteBufUtils.ReadUTF8(buffer);
            LastName = ByteBufUtils.ReadUTF8(buffer);
            LastMessage = ByteBufUtils.ReadUTF8(buffer);
            ConversationID = ByteBufUtils.ReadUTF8(buffer);
            PreviewCode = buffer.ReadInt();
            IsOnline = buffer.ReadBoolean();
            LastLogout = new DateTime(buffer.ReadLong());
            Relationship = buffer.ReadInt();

            Console.WriteLine(FirstName + " " + LastName);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            var id = ID;
            Console.WriteLine(Relationship); 

            Application.Current.Dispatcher.Invoke(() => UserList.Instance.AddUserToListView(
                new UserMessageViewModel()
                {
                    Id = id,
                    Name = FirstName + " " + LastName,
                    IsOnline = IsOnline,
                    IncomingMsg = PreviewCode == -1 ? String.Empty : (PreviewCode == 4 ? LastMessage : TranslatedPreviewCode[PreviewCode])
                }, Relationship == 2));

            if (Relationship == 2)
            {
                var app = MainWindow.chatApplication;
                if (!app.model.Conversations.ContainsKey(ConversationID))
                    app.model.Conversations.Add(ConversationID, new ConversationBubble());

                app.model.Conversations[ConversationID].Members.Clear();
                app.model.Conversations[ConversationID].Members.Add(ID);

                if (!app.model.PrivateConversations.ContainsKey(ID))
                    app.model.PrivateConversations.Add(ID, ConversationID);

                if (!app.model.IsOnline.ContainsKey(id))
                    app.model.IsOnline.Add(id, IsOnline);
            }
        }
    }
}
