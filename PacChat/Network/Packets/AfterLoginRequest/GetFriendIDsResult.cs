using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
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
    public class GetFriendIDsResult : IPacket
    {
        public List<string> ids { get; set; } = new List<string>();

        public void Decode(IByteBuffer buffer)
        {
            string id;
            id = ByteBufUtils.ReadUTF8(buffer);

            while (id != "~")
            {
                ids.Add(id);
                id = ByteBufUtils.ReadUTF8(buffer);
            }    
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            ChatModel.FriendIDs.Clear();

            foreach (var id in ids)
            {
                Console.WriteLine(id);
                ChatModel.FriendIDs.Add(id);

                // Get short info from ID
                GetShortInfo packet = new GetShortInfo();
                packet.ID = id;
                _ = ChatConnection.Instance.Send(packet);
            }

            Application.Current.Dispatcher.Invoke(() => UserListDesignModel.Instance.LoadContacts());
        }
    }
}
