using CNetwork;
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

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class GetConversationShortInfoResult : IPacket
    {
        public string ConversationID { get; set; }
        public string ConversationName { get; set; }
        public long LastActive { get; set; }

        public void Decode(IByteBuffer buffer)
        {
            ConversationID = ByteBufUtils.ReadUTF8(buffer);
            ConversationName = ByteBufUtils.ReadUTF8(buffer);
            LastActive = buffer.ReadLong();
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            Console.WriteLine("RecvConv: " + ConversationName);
            Application.Current.Dispatcher.Invoke(() =>
            {
                UserList.Instance.AddConversationToRecent(ConversationID, ConversationName, LastActive);
            });
        }
    }
}
