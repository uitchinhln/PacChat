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

namespace PacChat.Network.Packets.AfterLoginRequest.Message
{
    public class RecentConversationsResult : IPacket
    {
        public Dictionary<string, long> Conversations { get; set; } = new Dictionary<string, long>();

        public void Decode(IByteBuffer buffer)
        {
            string id = ByteBufUtils.ReadUTF8(buffer);
            long lastActive;

            while (id != "~")
            {
                lastActive = buffer.ReadLong();
                Conversations.Add(id, lastActive);
                id = ByteBufUtils.ReadUTF8(buffer);
            }
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            foreach (var x in Conversations)
                Console.WriteLine("Conv: " + x.Key);
            // Send IDs to get conversation name
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var x in Conversations)
                {
                    GetConversationShortInfo packet = new GetConversationShortInfo();
                    packet.ConversationID = x.Key;
                    _ = ChatConnection.Instance.Send(packet);
                }
            });
        }
    }
}
