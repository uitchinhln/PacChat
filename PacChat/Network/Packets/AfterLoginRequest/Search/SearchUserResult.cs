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

namespace PacChat.Network.Packets.AfterLoginRequest.Search
{
    public class SearchUserResult : IPacket
    {
        public List<String> UserIDs = new List<String>();

        public void Decode(IByteBuffer buffer)
        {
            string id = ByteBufUtils.ReadUTF8(buffer);

            while (id != "~")
            {
                UserIDs.Add(id);
                id = ByteBufUtils.ReadUTF8(buffer);
            }
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            Application.Current.Dispatcher.Invoke(() => UserList.Instance.ClearListView());

            if (UserIDs.Count > 20) return;

            foreach (var id in UserIDs)
            {
                GetShortInfo packet = new GetShortInfo();
                packet.ID = id;
                _ = ChatConnection.Instance.Send(packet);
            }
        }
    }
}
