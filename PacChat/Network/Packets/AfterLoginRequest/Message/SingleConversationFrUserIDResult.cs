using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class SingleConversationFrUserIDResult : IPacket
    {
        public string UserID { get; set; }
        public string ConversationID { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            UserID = ByteBufUtils.ReadUTF8(buffer);
            ConversationID = ByteBufUtils.ReadUTF8(buffer);
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            // throw new NotImplementedException();
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            var app = MainWindow.chatApplication;
            app.model.ContactsMessages[UserID].ConversationID = ConversationID;

            ChatPage.Instance.LoadChatPage(UserID);
        }
    }
}
