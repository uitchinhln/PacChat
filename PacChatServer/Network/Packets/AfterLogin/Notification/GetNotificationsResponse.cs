using CNetwork;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network.Packets.AfterLogin.Notification
{
    public class GetNotificationsResponse : IPacket
    {
        public List<string> Notifications { get; set; } = new List<string>();

        public void Decode(IByteBuffer buffer)
        {
        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            foreach (var noti in Notifications)
            {
                ByteBufUtils.WriteUTF8(byteBuf, noti);
            }
            ByteBufUtils.WriteUTF8(byteBuf, "~");
            return byteBuf;
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
