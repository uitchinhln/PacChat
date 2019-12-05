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
        public int Relationship { get; set; }

        //private Dictionary<int, string> TranslatedPreviewCode = new Dictionary<int, string>()
        //{
        //    {0, "Hidden message" },
        //    {1, "Attachment" },
        //    {2, "You got an image message" },
        //    {3, "You got a sticker message" },
        //    {5, "Video"}
        //};

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
            Console.WriteLine(PreviewCode); 

            Application.Current.Dispatcher.Invoke(() => UserList.Instance.AddUserToListView(
                new UserMessageViewModel()
                {
                    Id = id,
                    Name = FirstName + " " + LastName,
                    IsOnline = IsOnline,
                    //IncomingMsg = PreviewCode == 4 ? LastMessage : TranslatedPreviewCode[PreviewCode]
                    IncomingMsg = ""
                }));

            if (Relationship == 2)
            {
                var app = MainWindow.chatApplication;
                if (!app.model.ContactsMessages.ContainsKey(id))
                    app.model.ContactsMessages.Add(id, new ConversationBubble(ConversationID));

                if (!app.model.IsOnline.ContainsKey(id))
                    app.model.IsOnline.Add(id, IsOnline);
            }
        }
    }
}
