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

namespace PacChat.Network.Packets.AfterLoginRequest
{
    public class GetShortInfoResult : IPacket
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LastMessage { get; set; }
        public int PreviewCode { get; set; }
        public int IsOnline { get; set; }

        private Dictionary<int, string> TranslatedPreviewCode = new Dictionary<int, string>()
        {
            {0, "Hidden message" },
            {1, "Attachment" },
            {2, "You got an image message" },
            {3, "You got a sticker message" },
            {5, "Video"}
        };

        public void Decode(IByteBuffer buffer)
        {
            ID = ByteBufUtils.ReadUTF8(buffer);
            FirstName = ByteBufUtils.ReadUTF8(buffer);
            LastName = ByteBufUtils.ReadUTF8(buffer);
            LastMessage = ByteBufUtils.ReadUTF8(buffer);
            PreviewCode = buffer.ReadInt();
            IsOnline = buffer.ReadInt();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            var id = ID;
            ChatModel.FriendShortProfiles.Add(ID, 
                new Utils.UserUtils.ShortProfile() 
                { 
                    ID = id,
                    Name = FirstName + " " + LastName,
                    IncomingMsg = PreviewCode == 4 ? LastMessage : TranslatedPreviewCode[PreviewCode]
                });
        }
    }
}
